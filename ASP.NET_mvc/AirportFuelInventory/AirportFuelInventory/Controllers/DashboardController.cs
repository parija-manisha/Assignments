using AirportFuelInventory.Business;
using AirportFuelInventory.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirportFuelInventory.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard

        public ActionResult Dashboard()
        {
            var availableFuelData = AirportLogic.GetAvailableFuel();

            return View(availableFuelData);
        }

        public ActionResult ExportToPdf()
        {
            var report = new ActionAsPdf("Dashboard");
            return report;
        }
    }
}