using AirportFuelInventory.Attributes;
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
        [CustomAuthorize]
        public ViewResult AircraftList(int? page)
        {
            int currentPage = page ?? 1;
            var model = AircraftLogic.GetAircraftList(start: (currentPage - 1) * 5, length: 5);

            double totalRecords = AircraftLogic.GetTotalRecords() / 5.0;
            int totalPages = Convert.ToInt32(totalRecords);

            foreach (var aircraft in model)
            {
                aircraft.Pagination = new Pagination();
                aircraft.Pagination.CurrentPage = currentPage;
                aircraft.Pagination.TotalPages = totalPages;
            }

            return View(model);
        }

        [CustomAuthorize]
        public ViewResult AddAircraft(int? Aircraft_Id)
        {
            var model = new AircraftDTO
            {
                Sources = SourceLogic.GetSourceList(),
                Destinations = DestinationLogic.GetDestinationList(),
            };

            if (Aircraft_Id.HasValue)
            {
                var aircraft = AircraftLogic.GetAircraftDetailById(Convert.ToInt32(Request.QueryString["Aircraft_id"]));

                if (aircraft != null)
                {
                    model = aircraft;
                }
            }

            model.Sources = model.Sources ?? new List<SourceDTO>();
            model.Destinations = model.Destinations ?? new List<DestinationDTO>();

            return View(model);
        }

        [CustomAuthorize]
        public ActionResult NewAircraft(AircraftDTO aircraftDTO)
        {
            var addAirportSuccess = AircraftLogic.NewAircraft(aircraftDTO);
            if (addAirportSuccess)
            {
                return RedirectToAction("AircraftList");
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to register";
                return View();
            }
        }
    }
}