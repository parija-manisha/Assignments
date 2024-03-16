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
        public void TestAircraftList_ReturnView()
        {
            //Arrange
            AircraftController aircraftController = new AircraftController();

            //Act
            var result = aircraftController.AircraftList(null);

            //Assert
            Assert.IsNotNull(result);

            // Check if the model returned by the action is not null and is of type List<AirportSummary>
            var model = result.Model as List<Models.Model.AircraftDTO>; 
            Assert.IsNotNull(model);
            // Verify that the model contains at least one record
            Assert.IsTrue(model.Count > 0, "The model is having zero record.");
        }


        [TestMethod]
        public void TestNewAircraft_RedirectToAircraftList()
        {
            //Arrange
            AircraftController aircraftController = new AircraftController();

            //Mock Aircraft Details
            var aircraft = new Models.Model.AircraftDTO
            {
                Aircraft_no = "6EA123",
                Airline = "Indigo",
                Source_id = 1,
                Destination_id = 2,
            };

            //Act
            var result = aircraftController.NewAircraft(aircraft);

            //Assert
            Assert.IsNotNull(result);
            if (result is RedirectToRouteResult)
            {
                var redirectResult = (RedirectToRouteResult)result;
                Assert.AreEqual("AircraftList", redirectResult.RouteValues["action"]);
            }
            //If result is ViewResult, it means that addition of aircraft failed
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
