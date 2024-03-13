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
    public class AircraftControllerTest
    {
        [TestMethod]
        public void AircraftList_ReturnView()
        {
            AircraftController aircraftController = new AircraftController();
            var result = aircraftController.AircraftList(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddAircraft_ReturnView()
        {
            AirportController aircraftController = new AirportController();
            var result = aircraftController.AddAirport(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NewAircraft_RedirectToAircraftList()
        {
            AircraftController aircraftController = new AircraftController();

            var aircraft = new Models.Model.AircraftDTO
            {
                Aircraft_no = "6EA123",
                Airline = "Indigo",
                Source_id = 1,
                Destination_id = 2,
            };

            var result = aircraftController.NewAircraft(aircraft);

            Assert.IsNotNull(result);
            if (result is RedirectToRouteResult)
            {
                var redirectResult = (RedirectToRouteResult)result;

                Assert.AreEqual("AircraftList", redirectResult.RouteValues["action"]);
            }
            else if (result is ViewResult)
            {
                Assert.IsTrue(aircraftController.ViewData.ContainsKey("ErrorMessage"));
            }
            else
            {
                Assert.Fail("Unexpected result type");
            }
        }

    }
}
