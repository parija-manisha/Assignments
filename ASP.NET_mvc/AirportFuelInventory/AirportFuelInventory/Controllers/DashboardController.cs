using AirportFuelInventory.Attributes;
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
        [CustomAuthorize]
        public ViewResult Dashboard(int? page)
        {
            int currentPage = page ?? 1;
            var availableFuelData = AirportLogic.GetAvailableFuel(start: (currentPage - 1) * 5, length: 5);

            double totalRecords = AirportLogic.GetTotalRecords() / 5.0;
            int totalPages = Convert.ToInt32(totalRecords);
            foreach (var airport in availableFuelData)
            {
                airport.CurrentPage = currentPage;
                airport.TotalPages = totalPages;
            }

            return View(availableFuelData);
        }
    }
}