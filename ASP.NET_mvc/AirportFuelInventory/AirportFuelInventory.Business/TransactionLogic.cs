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
        public static void NewTransaction(TransactionDTO transactionDTO)
        {
            TransactionDataAccess.NewTransaction(transactionDTO);
        }

        public static List<TransactionDTO> GetTransactionList()
        {
            List<Transaction> transactions = TransactionDataAccess.GetTransactionList();
            List<TransactionDTO> transactionList = transactions.Select(transaction => new TransactionDTO
            {
                Transaction_Id = transaction.Transaction_Id,
                Transaction_date_time = transaction.Transaction_date_time,
                Transaction_type = transaction.Transaction_type,
                Airport_id = transaction.Airport_id,
                Aircraft_id = transaction.Aircraft_id,
                Quantity = transaction.Quantity,
                Transaction_id_parent = transaction.Transaction_id_parent,

            }).ToList();

            return transactionList;
        }

        public static void ReverseTransaction(TransactionDTO transactionDTO)
        {
            TransactionDataAccess.ReverseTransaction(transactionDTO);
        }

        public static TransactionDTO GetTransactionById(int transactionId)
        {
            return TransactionDataAccess.GetTransactionById(transactionId);
        }
    }
}