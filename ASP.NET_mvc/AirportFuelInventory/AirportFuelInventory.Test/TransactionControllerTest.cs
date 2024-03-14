using AirportFuelInventory.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AirportFuelInventory.Test
{
    [TestClass]
    public class TransactionControllerTest
    {
        [TestMethod]
        public void Transaction_ReturnView()
        {
            TransactionController transactionController = new TransactionController();
            var result = transactionController.Transaction();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Transaction_RedirectToTransactionAction()
        {
            TransactionController transactionController = new TransactionController();
            var transaction = new Models.Model.TransactionDTO
            {
                Transaction_date_time = DateTime.Now,
                Transaction_type = 1,
                Aircraft_id = 1,
                Airport_id = 1,
                Quantity = 100,
            };

            var result = transactionController.AddTransaction(null, transaction);

            Assert.IsNotNull(result);
            if (result is RedirectToRouteResult)
            {
                var redirectRoute = (RedirectToRouteResult)result;
                Assert.AreEqual("Transaction", redirectRoute.RouteValues["action"]);
            }

            else if (result is ViewResult)
            {
                Assert.IsTrue(transactionController.ViewData.ContainsKey("ErrorMessage"));
            }

            else
            {
                Assert.Fail("Unexpected result type");
            }
        }
    }
}
