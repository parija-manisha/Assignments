using AirportFuelInventory.Business;
using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Transaction()
        {
            var model = new TransactionDTO
            {
                Transactions = TransactionLogic.GetTransactionList()
            };

            model.Transactions = model.Transactions ?? new List<TransactionDTO>();

            return View(model);
        }

        public ActionResult AddTransaction(int? Transaction_Id)
        {
            var model = new TransactionDTO
            {
                TransactionTypes = Constants.TransactionTypes
            };

            if (Transaction_Id.HasValue)
            {
                var transaction = TransactionLogic.GetTransactionById(Convert.ToInt32(Request.QueryString["Transaction_Id"]));

                if (transaction != null)
                {
                    model = transaction;
                }
            }

            model.TransactionTypes = model.TransactionTypes ?? new List<TransactionType>();

            return View(model);

        }

        [HttpPost]
        public ActionResult AddTransaction(TransactionDTO transactionDTO)
        {
            if (transactionDTO.Transaction_Id != 0)
            {
                TempData["UpdatedTransactionDTO"] = transactionDTO;
                return RedirectToAction("ReverseTransaction", "Transaction");
            }
            else
            {
                TransactionLogic.NewTransaction(transactionDTO);
            }

            return RedirectToAction("Transaction");
        }

        public ActionResult ReverseTransaction()
        {
            var updatedTransactionDTO = TempData["UpdatedTransactionDTO"] as TransactionDTO;

            TransactionLogic.ReverseTransaction(updatedTransactionDTO);
            return RedirectToAction("Transaction", "Transaction");
        }
    }
}