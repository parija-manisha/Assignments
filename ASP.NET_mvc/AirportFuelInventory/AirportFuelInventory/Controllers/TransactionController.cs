using AirportFuelInventory.Attributes;
using AirportFuelInventory.Business;
using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        [CustomAuthorize]
        public ViewResult Transaction(int? page)
        {
            int currentPage = page ?? 1;
            var model = TransactionLogic.GetTransactionList(start: (currentPage - 1) * 5, length: 5);

            double totalRecords = TransactionLogic.GetTotalRecords() / 5.0;
            int totalPages = Convert.ToInt32(totalRecords);


            foreach (var transaction in model)
            {
                transaction.Pagination = new Pagination();
                transaction.Pagination.CurrentPage = currentPage;
                transaction.Pagination.TotalPages = totalPages;
            }

            return View(model);
        }

        [CustomAuthorize]
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

        [CustomAuthorize]
        public ActionResult DeleteTransaction()
        {
            var deleteSuccess = TransactionLogic.DeleteTransaction();
            if (deleteSuccess)
            {
                return RedirectToAction("Transaction");
            }

            else
            {
                ViewBag.ErrorMessage = "No Transaction found to be deleted";
                return View("Transaction");
            }
        }
    }
}