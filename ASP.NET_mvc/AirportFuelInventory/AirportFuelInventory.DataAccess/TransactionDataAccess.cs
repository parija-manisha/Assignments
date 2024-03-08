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
        public static void NewTransaction(TransactionDTO transactionDTO)
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
            }
        }

        public static List<Transaction> GetTransactionList()
        {
            List<Transaction> transactionList = new List<Transaction>();
            using (var context = new AirportFuelInventoryEntities())
            {
                transactionList = context.Transactions.ToList();
            }

            return transactionList;
        }

        public static void ReverseTransaction(TransactionDTO transactionDTO)
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
            }
        }

        public static TransactionDTO GetTransactionById(int transactionId)
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
                        Transaction_id_parent= transactionDetails.Transaction_Id,
                    };

                    return transaction;
                }

                return null;
            }

        }
    }
}
