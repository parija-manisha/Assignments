using AirportFuelInventory.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelInventory.Test
{
    [TestClass]
    public class FuelConsumptionReportControllerTest
    {
        [TestMethod]
        public void FuelConsumptionReport_ReturnView()
        {
            FuelConsumptionReportController fuelConsumptionReportController = new FuelConsumptionReportController();
            var result = fuelConsumptionReportController.FuelConsumptionReport(null);

            Assert.IsNotNull(result);
        }

    }
}
