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
    public class DashboardControllerTest
    {
        [TestMethod]
        public void Dashboard_ReturnView()
        {
            DashboardController dashboardController = new DashboardController();
            var result = dashboardController.Dashboard(null);

            Assert.IsNotNull(result);
        }

    }
}
