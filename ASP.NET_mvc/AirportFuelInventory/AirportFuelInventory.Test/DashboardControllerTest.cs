using AirportFuelInventory.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelInventory.Test
{
    /// <summary>
    /// Tests the Dashboard action method of the DashboardController to ensure it returns a view with a non-null model containing at least one record.
    /// </summary>
    [TestClass]
    public class DashboardControllerTest
    {
        [TestMethod]
        public void TestDashboard_ReturnView()
        {
            //Arrange
            DashboardController dashboardController = new DashboardController();

            //Act
            var result = dashboardController.Dashboard(null);

            //Assert
            Assert.IsNotNull(result);

            // Check if the model returned by the action is not null and is of type List<AirportSummary>
            var model = result.Model as List<Models.ReportSummary.AirportSummary>;
            Assert.IsNotNull(model);

            // Verify that the model contains at least one record
            Assert.IsTrue(model.Count > 0, "The model is having zero record.");
        }

    }
}
