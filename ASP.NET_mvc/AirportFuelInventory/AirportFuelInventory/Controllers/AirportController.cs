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
    public class AirportController : Controller
    {
        // GET: AirportList
        public ActionResult AirportList()
        {
            var model = new AirportDTO
            {
                Airports = AirportLogic.GetAirportList()
            };

            model.Airports = model.Airports ?? new List<AirportDTO>();

            return View(model);
        }

        public ActionResult AddAirport() { 
            return View();
        }

        public ActionResult NewAirport(AirportDTO airportDTO)
        {
            AirportLogic.NewAirport(airportDTO);
            return RedirectToAction("AirportList");
        }
    }
}