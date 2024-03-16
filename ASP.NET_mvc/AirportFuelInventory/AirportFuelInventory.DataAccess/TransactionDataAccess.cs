using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.DataAccess
{
    public class TransactionDataAccess
    {
        public static bool NewTransaction(TransactionDTO transactionDTO)
        {
            try
            {
                using (var context = new AirportFuelInventoryEntities())
                {
                    Transaction transaction = new Transaction
                    {
                        Transaction_date_time = DateTime.Now,
                        Transaction_type = transactionDTO.Transaction_type,
                        Airport_id = transactionDTO.Airport_id,
                        Aircraft_id = transactionDTO.Aircraft_id,
                        Quantity = transactionDTO.Quantity,
                        Transaction_id_parent = transactionDTO.Transaction_Id != 0 ? (int?)transactionDTO.Transaction_Id : null,
                    };
                    context.Transactions.Add(transaction);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not add transaction list", ex);
                return false;
            }
        }

        public static List<TransactionDTO> GetTransactionList(int start, int length)
        {
            try
            {
                using (var context = new AirportFuelInventoryEntities())
                {
                    var transactionEntities = context.Transactions
                        .OrderByDescending(t => t.Transaction_date_time)
                        .Skip(start)
                        .Take(length)
                        .ToList();

                    List<TransactionDTO> transactionList = transactionEntities.Select(transaction => new TransactionDTO
                    {
                        Transaction_date_time = transaction.Transaction_date_time,
                        Transaction_type = transaction.Transaction_type,
                        Aircraft_id = transaction.Aircraft_id,
                        Airport_id = transaction.Airport_id,
                        Quantity = transaction.Quantity,
                        AircraftDTOs = transaction.Aircraft != null ? new List<AircraftDTO> { new AircraftDTO
                        {
                              Aircraft_id = transaction.Aircraft_id,
                              Aircraft_no = transaction.Aircraft.Aircraft_no
                        } } : null,
                        AirportDTOs = transaction.Airport != null ? new List<AirportDTO> { new AirportDTO
                        {
                               Airport_id = transaction.Airport_id,
                               Airport_name = transaction.Airport.Airport_name,
                        } } : null
                    }).ToList();

                    return transactionList;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch transaction list", ex);
                return null;
            }

        }

        public static TransactionDTO GetTransactionById(int transactionId)
        {
            try
            {
                using (AirportFuelInventoryEntities context = new AirportFuelInventoryEntities())
                {
                    var transactionDetails = context.Transactions
                        .FirstOrDefault(u => u.Transaction_id == transactionId);

                    if (transactionDetails != null)
                    {
                        var transaction = new TransactionDTO
                        {
                            Transaction_Id = transactionId,
                            Transaction_date_time = transactionDetails.Transaction_date_time,
                            TransactionTypes = Constants.TransactionTypes,
                            Transaction_type = transactionDetails.Transaction_type,
                            Airport_id = transactionDetails.Airport_id,
                            Aircraft_id = transactionDetails.Aircraft_id,
                            AircraftDTOs = new List<AircraftDTO>(),
                            AirportDTOs = new List<AirportDTO>(),
                            Quantity = transactionDetails.Quantity,
                            Transaction_id_parent = transactionDetails.Transaction_id,
                        };

                        transaction.AirportDTOs.Add(new AirportDTO
                        {
                            Airport_id = transactionDetails.Airport_id,
                            Airport_name = transactionDetails.Airport.Airport_name
                        });

                        transaction.AircraftDTOs.Add(new AircraftDTO
                        {
                            Aircraft_id = transactionDetails.Aircraft_id,
                            Aircraft_no = transactionDetails.Aircraft.Aircraft_no
                        });

                        return transaction;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch transaction details", ex);
                return null;
            }
        }

        public static bool DeleteTransaction()
        {
            try
            {
                using (var context = new AirportFuelInventoryEntities())
                {
                    var allTransactions = context.Transactions.ToList();
                    if (allTransactions.Any())
                    {
                        context.Transactions.RemoveRange(allTransactions);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Failed to delete", ex);
                return false;
            }
        }

        public static double GetTotalRecords()
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                var count = context.Transactions.Count();
                return count;
            }
        }
    }
}
