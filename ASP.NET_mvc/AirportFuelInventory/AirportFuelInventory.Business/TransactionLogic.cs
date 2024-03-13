using AirportFuelInventory.DataAccess;
using AirportFuelInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Business
{
    public class TransactionLogic
    {
        public static bool NewTransaction(TransactionDTO transactionDTO)
        {
            return TransactionDataAccess.NewTransaction(transactionDTO);
        }

        public static List<TransactionDTO> GetTransactionList()
        {
            List<TransactionDTO> transactions = TransactionDataAccess.GetTransactionList();

            return transactions;
        }

        public static TransactionDTO GetTransactionById(int transactionId)
        {
            return TransactionDataAccess.GetTransactionById(transactionId);
        }

        public static bool DeleteTransaction()
        {
            return TransactionDataAccess.DeleteTransaction();
        }
    }
}