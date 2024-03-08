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

        public static List<object> GetAvailableFuel()
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                var availableFuel = context.Transactions
                    .Where(t => t.Transaction_type == (int)Constants.TransactionType.In || t.Transaction_type == (int)Constants.TransactionType.Out)
                    .GroupBy(t => t.Airport_id)
                    .Select(group => new
                    {
                        AirportId = group.Key,
                        AvailableFuel = group.Sum(t => t.Transaction_type == (int)Constants.TransactionType.In ? t.Quantity : -t.Quantity)
                    })
                    .ToList();

                var airports = context.Airports.ToDictionary(a => a.Airport_Id, a => a);

                return availableFuel
                    .Select(data => new
                    {
                        AirportId = data.AirportId,
                        AirportName = airports.ContainsKey(data.AirportId) ? airports[data.AirportId].Airport_Name : "Unknown",
                        AvailableFuel = data.AvailableFuel
                    })
                    .ToList<object>();
            }
        }
    }
}
