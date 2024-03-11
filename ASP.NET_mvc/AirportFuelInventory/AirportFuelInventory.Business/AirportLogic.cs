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

        public static bool NewAirport(AirportDTO airport)
        {
            return AirportDataAccess.NewAirport(airport);
        }

        public static List<AirportDTO> GetAirportNameList()
        {
            List<Airport> airportName = AirportDataAccess.GetAirportNameList();
            List<AirportDTO> airportDTOs = airportName.Select(airport => new AirportDTO
            {
                Airport_Name = airport.Airport_Name,
                Airport_Id = airport.Airport_Id,
            }).ToList();

            return airportDTOs;
        }

        public static List<ReportSummary.AirportSummary> GetAvailableFuel()
        {
            return AirportDataAccess.GetAvailableFuel();
        }
        public static List<ReportSummary.FuelSummary> GetFuelConsumptionReport()
        {
            return AirportDataAccess.GetFuelConsumptionReport();
        }
    }
}
