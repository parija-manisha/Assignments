using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static List<Transaction> GetTransactionList()
        {
            try
            {
                List<Transaction> transactionList;
                using (var context = new AirportFuelInventoryEntities())
                {
                    transactionList = context.Transactions
                        .OrderByDescending(t => t.Transaction_date_time)
                        .ToList();
                }

                return transactionList;
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch transaction list", ex);
                return null;
            }
        }


        public static bool ReverseTransaction(TransactionDTO transactionDTO)
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
                        Transaction_id_parent = transactionDTO.Transaction_Id,
                    };
                    context.Transactions.Add(transaction);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch aircraft list", ex);
                return false;
            }
        }

        public static TransactionDTO GetTransactionById(int transactionId)
        {
            try
            {
                using (AirportFuelInventoryEntities context = new AirportFuelInventoryEntities())
                {
                    var transactionDetails = context.Transactions
                        .FirstOrDefault(u => u.Transaction_Id == transactionId);

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
                            Quantity = transactionDetails.Quantity,
                            Transaction_id_parent = transactionDetails.Transaction_Id,
                        };

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
                    context.Transactions.RemoveRange(allTransactions);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Failed to delete", ex);
                return false;
            }
        }
    }
}
