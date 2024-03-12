using AirportFuelInventory.Business;
using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
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
        public ActionResult AirportList(int? page, string sortColumn, string sortDirection)
        {
            int currentPage = page ?? 1;
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "Airport_Name" : sortColumn;
            sortDirection = Constants.ToggleSortDirection(sortDirection);
            var model = AirportLogic.GetAirportList(start: (currentPage - 1) * 5, length: 5, sortColumn: sortColumn, sortDirection: sortDirection);

            double totalRecords = AirportLogic.GetTotalRecords() / 5.0;
            int totalPages = Convert.ToInt32(totalRecords);


            foreach (var airport in model)
            {
                airport.Pagination = new Pagination(); airport.Pagination.CurrentPage = currentPage;
                airport.Pagination.TotalPages = totalPages;
            }

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