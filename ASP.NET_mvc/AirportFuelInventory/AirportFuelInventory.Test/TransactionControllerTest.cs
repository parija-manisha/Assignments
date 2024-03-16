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
        public void TestTransaction_ReturnsView()
        {
            //Arrange
            TransactionController transactionController = new TransactionController();
            //Act
            var result = transactionController.Transaction(null);

            //Assert
            Assert.IsNotNull(result);

            // Check if the model returned by the action is not null and is of type List<AirportSummary>
            var model = result.Model as List<Models.Model.TransactionDTO>;
            Assert.IsNotNull(model);

            // Verify that the model contains at least one record
            Assert.IsTrue(model.Count > 0, "The model is having zero record.");
        }

        [TestMethod]
        public void TestAddTransaction_Returns_RedirectToTransactionAction()
        {
            //Arrange
            TransactionController transactionController = new TransactionController();

            //Mock Transaction Details to be added 
            var transaction = new Models.Model.TransactionDTO
            {
                Transaction_date_time = DateTime.Now,
                Transaction_type = 1,
                Aircraft_id = 1,
                Airport_id = 1,
                Quantity = 100,
            };

            //Act
            var result = transactionController.AddTransaction(null, transaction);

            //Assert
            Assert.IsNotNull(result);
            if (result is RedirectToRouteResult)
            {
                var redirectRoute = (RedirectToRouteResult)result;
                Assert.AreEqual("Transaction", redirectRoute.RouteValues["action"]);
            }

            // If the result is a ViewResult, it suggests that either the action resulted in rendering a new transaction addition page or that during the reverse transaction, there was a failure to add the reverse transaction.
            else if (result is ViewResult)
            {
                Assert.IsTrue(transactionController.ViewData.ContainsKey("ErrorMessage"));
            }

            else
            {
                Assert.Fail("Unexpected result type");
            }
        }

        [TestMethod]
        public void TestDeleteTransaction_ReturnsView()
        {
            // Arrange
            TransactionController transactionController = new TransactionController();

            // Act
            var result = transactionController.DeleteTransaction();

            // Assert
            Assert.IsNotNull(result);

            if (result is RedirectToRouteResult redirectRoute)
            {
                Assert.AreEqual("Transaction", redirectRoute.RouteValues["action"]);
            }

            //If the result is ViewResult, it means that there is no record to be deleted
            else if (result is ViewResult viewResult)
            {
                Assert.AreEqual("Transaction", viewResult.ViewName);
                //Assert.AreEqual("No Transaction found to be deleted", viewResult.ViewBag);
                Assert.IsTrue(transactionController.ViewData.ContainsKey("ErrorMessage"));
            }
            else
            {
                Assert.Fail("Result Type is not valid");
            }
        }
    }
}
