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
        public void TestAirportList_ReturnView()
        {
            //Arrange
            AirportController aircraftController = new AirportController();

            //Act
            var result = aircraftController.AirportList(null);

            //Assert
            Assert.IsNotNull(result);

            // Check if the model returned by the action is not null and is of type List<AirportSummary>
            var model = result.Model as List<Models.Model.AirportDTO>;
            Assert.IsNotNull(model);
            // Verify that the model contains at least one record
            Assert.IsTrue(model.Count > 0, "The model is having zero record.");
        }

        [TestMethod]
        public void AddAirport_RedirectToAircraftList()
        {
            //Act
            AirportController airportController = new AirportController();

            //Mock Airport Details
            var airport = new Models.Model.AirportDTO
            {
                Airport_name = "Test",
                Fuel_Capacity = 100,
            };

            //Act
            var result = airportController.NewAirport(airport);

            //Assert
            Assert.IsNotNull(result);
            if (result is RedirectToRouteResult)
            {
                var redirectResult = (RedirectToRouteResult)result;
                Assert.AreEqual("AirportList", redirectResult.RouteValues["action"]);
            }
            //If result is a ViewResult, it means that adding new airport failed
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
