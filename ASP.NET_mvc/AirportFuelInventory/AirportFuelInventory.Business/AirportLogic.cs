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
        public static List<AirportDTO> GetAirportList(int start, int length)
        {
            List<Airport> airports = AirportDataAccess.GetAirportList(start, length);
            List<AirportDTO> airportList = airports.Select(airport => new AirportDTO
            {
                Airport_id = airport.Airport_id,
                Airport_name = airport.Airport_name,
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
                Airport_name = airport.Airport_name,
                Airport_id = airport.Airport_id,
            }).ToList();

            return airportDTOs;
        }

        public static List<ReportSummary.AirportSummary> GetAvailableFuel(int start, int length)
        {
            return AirportDataAccess.GetAvailableFuel(start, length);
        }
        public static List<ReportSummary.FuelSummary> GetFuelConsumptionReport(int start, int length)
        {
            return AirportDataAccess.GetFuelConsumptionReport(start, length);
        }

        public static double GetTotalRecords()
        {
            return AirportDataAccess.GetTotalRecords();
        }

        public static AirportDTO GetAirportDetailById(int airportId)
        {
            return AirportDataAccess.GetAirportDetailById(airportId);
        }
    }
}
