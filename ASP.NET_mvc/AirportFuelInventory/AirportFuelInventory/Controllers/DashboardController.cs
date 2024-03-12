using AirportFuelInventory.Business;
using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
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

        public ActionResult Dashboard(int? page, string sortColumn, string sortDirection)
        {
            int currentPage = page ?? 1;
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "AirportName" : sortColumn; 
            sortDirection = Constants.ToggleSortDirection(sortDirection);
            var availableFuelData = AirportLogic.GetAvailableFuel(start: (currentPage - 1) * 5, length: 5, sortColumn:sortColumn,sortDirection:sortDirection);

            double totalRecords = AirportLogic.GetTotalRecords() / 5.0;
            int totalPages = Convert.ToInt32(totalRecords);
            foreach (var airport in availableFuelData)
            {
                airport.CurrentPage = currentPage;
                airport.TotalPages = totalPages;
            }

            return View(availableFuelData);
        }

        public ActionResult ExportToPdf()
        {
            var report = new ActionAsPdf("Dashboard");
            return report;
        }
    }
}