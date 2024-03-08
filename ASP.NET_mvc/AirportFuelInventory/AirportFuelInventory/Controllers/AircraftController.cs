using AirportFuelInventory.Business;
using AirportFuelInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Controllers
{
    public class AircraftController : Controller
    {
        // GET: Aircraft
        public ActionResult AircraftList()
        {
            var model = new AircraftDTO
            {
                Aircrafts = AircraftLogic.GetAircraftList()
            };
            return View(model);
        }

        public ActionResult AddAircraft()
        {
            return View();
        }

        public ActionResult NewAircraft(AircraftDTO aircraftDTO)
        {
            AircraftLogic.NewAircraft(aircraftDTO);
            return RedirectToAction("AircraftList");
        }
    }
}