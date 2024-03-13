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
        public ActionResult AirportList(int? page)
        {
            int currentPage = page ?? 1;
            var model = AirportLogic.GetAirportList(start: (currentPage - 1) * 5, length: 5);

            double totalRecords = AirportLogic.GetTotalRecords() / 5.0;
            int totalPages = Convert.ToInt32(totalRecords);


            foreach (var airport in model)
            {
                airport.Pagination = new Pagination(); airport.Pagination.CurrentPage = currentPage;
                airport.Pagination.TotalPages = totalPages;
            }

            return View(model);
        }

        public ActionResult AddAirport(int? Airport_Id) {
            var model = new AirportDTO();
            if (Airport_Id.HasValue)
            {
                var airport = AirportLogic.GetAirportDetailById(Convert.ToInt32(Request.QueryString["Airport_Id"]));

                if (airport != null)
                {
                    model = airport;
                }
            }

            return View(model);
        }

        public ActionResult NewAirport(AirportDTO airportDTO)
        {
            AirportLogic.NewAirport(airportDTO);
            return RedirectToAction("AirportList");
        }
    }
}