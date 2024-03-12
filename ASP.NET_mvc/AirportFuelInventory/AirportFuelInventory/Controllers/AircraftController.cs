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
    public class AircraftController : Controller
    {
        // GET: Aircraft
        public ActionResult AircraftList(int? page, string sortColumn, string sortDirection)
        {
            int currentPage = page ?? 1;
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "Aircraft_Name" : sortColumn; 
            sortDirection = Constants.ToggleSortDirection(sortDirection);
            var model = AircraftLogic.GetAircraftList(start: (currentPage - 1) * 5, length: 5, sortColumn: sortColumn, sortDirection: sortDirection);

            int totalPages = AircraftLogic.GetTotalRecords() / 5;
            foreach (var aircraft in model)
            {
                aircraft.Pagination = new Pagination();                aircraft.Pagination.CurrentPage = currentPage;
                aircraft.Pagination.TotalPages = totalPages;
            }
            
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