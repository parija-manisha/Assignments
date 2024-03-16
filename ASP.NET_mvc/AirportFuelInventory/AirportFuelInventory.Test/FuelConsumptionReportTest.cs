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
    /// Tests the FuelConsumptionReport action method of the FuelConsumptionReportController to ensure it returns a view with a non-null model containing at least one record.
    /// </summary>
    [TestClass]
    public class FuelConsumptionReportControllerTest
    {
        [TestMethod]
        public void TestFuelConsumptionReport_ReturnView()
        {
            //Arrange
            FuelConsumptionReportController fuelConsumptionReportController = new FuelConsumptionReportController();

            //Act
            var result = fuelConsumptionReportController.FuelConsumptionReport(null);

            //Assert
            Assert.IsNotNull(result);

            // Check if the model returned by the action is not null and is of type List<FuelSummary>
            var model = result.Model as List<Models.ReportSummary.FuelSummary>;
            Assert.IsNotNull(model);

            // Verify that the model contains at least one record
            Assert.IsTrue(model.Count > 0, "The model is having zero record.");
        }

    }
}
