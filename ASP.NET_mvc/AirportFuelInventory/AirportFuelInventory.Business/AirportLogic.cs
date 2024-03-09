using AirportFuelInventory.DataAccess;
using AirportFuelInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Business
{
    public class AirportLogic
    {
        public static List<AirportDTO> GetAirportList()
        {
            List<Airport> airports = AirportDataAccess.GetAirportList();
            List<AirportDTO> airportList = airports.Select(airport => new AirportDTO
            {
                Airport_Name = airport.Airport_Name,
                Fuel_Capacity = airport.Fuel_Capacity
            }).ToList();

            return airportList;
        }

        public static void NewAirport(AirportDTO airport)
        {
            AirportDataAccess.NewAirport(airport);
        }

        public static List<ReportSummary> GetAvailableFuel()
        {
            return AirportDataAccess.GetAvailableFuel();
        }
    }
}
