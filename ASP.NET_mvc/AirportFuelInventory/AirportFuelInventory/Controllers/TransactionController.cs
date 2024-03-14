using AirportFuelInventory.Business;
using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
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
                Transactions = TransactionLogic.GetTransactionList(),
            };

            model.Transactions = model.Transactions ?? new List<TransactionDTO>();

            return View(model);
        }

        public ActionResult AddTransaction(int? Transaction_Id, TransactionDTO transactionDTO)
        {
            var model = new TransactionDTO
            {
                TransactionTypes = Constants.TransactionTypes,
                AirportDTOs = AirportLogic.GetAirportNameList(),
                AircraftDTOs = AircraftLogic.GetAircraftNameList()
            };

            if (Transaction_Id.HasValue)
            {
                var transaction = TransactionLogic.GetTransactionById(Convert.ToInt32(Request.QueryString["Transaction_Id"]));

                if (transaction != null)
                {
                    model = transaction;
                }

                else if (transactionDTO != transaction)
                {
                    var addTransactionSuccess = TransactionLogic.NewTransaction(transactionDTO);
                    if (addTransactionSuccess)
                    {
                        return RedirectToAction("Transaction");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to Add Transaction";
                        return View();
                    }
                }
            }
            model.TransactionTypes = model.TransactionTypes ?? new List<TransactionType>();

            //Added the ViewBag for unit testing
            ViewBag.ErrorMessage = "";

            return View(model);
        }

        public ActionResult DeleteTransaction()
        {
            var deleteSuccess = TransactionLogic.DeleteTransaction();
            if (deleteSuccess)
            {
                return View("Transaction");
            }

            else
            {
                return HttpNotFound("No transactions found to delete.");
            }
        }
    }
}