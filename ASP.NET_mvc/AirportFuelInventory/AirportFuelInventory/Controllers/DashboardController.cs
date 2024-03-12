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

        public ActionResult Dashboard(int? page, string sortColumn, string sortDirection)
        {
            int currentPage = page ?? 1;
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "AirportName" : sortColumn; ;
            sortDirection = ToggleSortDirection(sortDirection);
            var availableFuelData = AirportLogic.GetAvailableFuel(start: (currentPage - 1) * 5, length: 5, sortColumn:sortColumn,sortDirection:sortDirection);

            int totalPages = AirportLogic.GetTotalRecords() / 5;
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

        private string ToggleSortDirection(string currentDirection)
        {
            return currentDirection?.ToUpper() == "ASC" ? "DESC" : "ASC";
        }
    }
}