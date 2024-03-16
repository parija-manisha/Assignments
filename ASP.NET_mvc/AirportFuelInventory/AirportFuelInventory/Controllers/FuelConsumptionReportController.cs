using AirportFuelInventory.Attributes;
using AirportFuelInventory.Business;
using AirportFuelInventory.Utils;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirportFuelInventory.Controllers
{
    public class FuelConsumptionReportController : Controller
    {
        // GET: FuelConsumptionReport
        [CustomAuthorize]
        public ViewResult FuelConsumptionReport(int? page)
        {
            int currentPage = page ?? 1;
            var fuelConsumption = AirportLogic.GetFuelConsumptionReport(start: (currentPage - 1) * 5, length: 5);
            double totalRecords = AirportLogic.GetTotalRecords() / 5.0;
            int totalPages = Convert.ToInt32(totalRecords);

            foreach (var airport in fuelConsumption)
            {
                airport.CurrentPage = currentPage;
                airport.TotalPages = totalPages;
            }

            return View(fuelConsumption);
        }
    }
}