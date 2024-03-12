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
        public static List<AirportDTO> GetAirportList(int start, int length, string sortColumn, string sortDirection)
        {
            List<Airport> airports = AirportDataAccess.GetAirportList(start, length, sortColumn, sortDirection);
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

        public static List<ReportSummary.AirportSummary> GetAvailableFuel(int start, int length, string sortColumn, string sortDirection)
        {
            return AirportDataAccess.GetAvailableFuel(start, length, sortColumn, sortDirection);
        }
        public static List<ReportSummary.FuelSummary> GetFuelConsumptionReport(int start, int length, string sortColumn, string sortDirection)
        {
            return AirportDataAccess.GetFuelConsumptionReport(start, length, sortColumn, sortDirection);
        }

        public static double GetTotalRecords()
        {
            return AirportDataAccess.GetTotalRecords();
        }
    }
}
