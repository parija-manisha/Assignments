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
    public class AirportControllerTest
    {
        [TestMethod]
        public void AirportList_ReturnView()
        {
            AirportController aircraftController = new AirportController();
            var result = aircraftController.AirportList(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddAirport_RedirectToAircraftList()
        {
            AirportController airportController = new AirportController();

            var airport = new Models.Model.AirportDTO
            {
                Airport_name = "Test",
                Fuel_Capacity = 100,
            };

            var result = airportController.NewAirport(airport);

            Assert.IsNotNull(result);
            if (result is RedirectToRouteResult)
            {
                var redirectResult = (RedirectToRouteResult)result;

                Assert.AreEqual("AirportList", redirectResult.RouteValues["action"]);
            }
            else if (result is ViewResult)
            {
                Assert.IsTrue(airportController.ViewData.ContainsKey("ErrorMessage"));
            }
            else
            {
                Assert.Fail("Unexpected result type");
            }
        }
    }
}
