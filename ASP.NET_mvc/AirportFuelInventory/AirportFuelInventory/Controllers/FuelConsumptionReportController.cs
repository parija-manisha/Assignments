using AirportFuelInventory.Business;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirportFuelInventory.Controllers
{
    public class FuelConsumptionReportController : Controller
    {
        // GET: FuelConsumptionReport
        public ActionResult FuelConsumptionReport()
        {
            var fuelConsumption = AirportLogic.GetFuelConsumptionReport();

            return View(fuelConsumption);
        }

        public ActionResult ExportToPdf()
        {
            var report = new ActionAsPdf("FuelConsumptionReport");
            return report;
        }
    }
}