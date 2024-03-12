using AirportFuelInventory.Business;
using AirportFuelInventory.Utils;
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
        public ActionResult FuelConsumptionReport(int? page, string sortColumn, string sortDirection)
        {
            int currentPage = page ?? 1;
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "AirportName" : sortColumn; 
            sortDirection = Constants.ToggleSortDirection(sortDirection);
            var fuelConsumption = AirportLogic.GetFuelConsumptionReport(start: (currentPage - 1) * 5, length: 5, sortColumn: sortColumn, sortDirection: sortDirection);

            double totalRecords = AirportLogic.GetTotalRecords() / 5.0;
            int totalPages = Convert.ToInt32(totalRecords);

            foreach (var airport in fuelConsumption)
            {
                airport.CurrentPage = currentPage;
                airport.TotalPages = totalPages;
            }
           

            return View(fuelConsumption);
        }

        public ActionResult ExportToPdf()
        {
            var report = new ActionAsPdf("FuelConsumptionReport");
            return report;
        }
    }
}