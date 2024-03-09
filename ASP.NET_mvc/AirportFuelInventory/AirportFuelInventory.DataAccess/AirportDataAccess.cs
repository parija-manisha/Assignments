using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.DataAccess
{
    public class AirportDataAccess
    {
        public static List<Airport> GetAirportList()
        {
            List<Airport> airportList = new List<Airport>();
            using (var context = new AirportFuelInventoryEntities())
            {
                airportList = context.Airports.ToList();
            }

            return airportList;
        }

        public static void NewAirport(AirportDTO airportDto)
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                Airport airport = new Airport
                {
                    Airport_Name = airportDto.Airport_Name,
                    Fuel_Capacity = airportDto.Fuel_Capacity,
                };
                context.Airports.Add(airport);
                context.SaveChanges();
            }
        }

        public static List<ReportSummary> GetAvailableFuel()
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                var transactions = context.Transactions
                    .Where(t => t.Transaction_type == (int)Constants.TransactionType.In || t.Transaction_type == (int)Constants.TransactionType.Out)
                    .GroupBy(t => t.Airport_id)
                    .Select(group => new
                    {
                        AirportId = group.Key,
                        TotalQuantity = group.Sum(t => t.Quantity * (t.Transaction_type == (int)Constants.TransactionType.In ? 1 : -1))
                    })
                    .ToList();

                var airports = context.Airports.ToDictionary(a => a.Airport_Id, a => a);

                var result = transactions
                    .Where(t => airports.ContainsKey(t.AirportId))
                    .Select(t => new ReportSummary
                    {
                        AirportId = t.AirportId,
                        AirportName = airports[t.AirportId].Airport_Name,
                        AvailableFuel = airports[t.AirportId].Fuel_Capacity - t.TotalQuantity
                    })
                    .ToList();

                return result;
            }
        }
    }
}
