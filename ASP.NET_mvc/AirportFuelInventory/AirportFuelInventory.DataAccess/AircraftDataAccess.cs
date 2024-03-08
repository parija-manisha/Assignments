using AirportFuelInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.DataAccess
{
    public class AircraftDataAccess
    {
        public static List<Aircraft> GetAircraftList()
        {
            List<Aircraft> aircraftList = new List<Aircraft>();
            using (var context = new AirportFuelInventoryEntities())
            {
                aircraftList = context.Aircraft.ToList();
            }

            return aircraftList;
        }

        public static void NewAircraft(AircraftDTO aircraftDTO)
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                Aircraft aircraft = new Aircraft
                {
                    Aircraft_Name=aircraftDTO.Aircraft_Name,
                    Airline=aircraftDTO.Airline,
                    Source=aircraftDTO.Source,
                    Destination=aircraftDTO.Destination,
                };
                context.Aircraft.Add(aircraft);
                context.SaveChanges();
            }
        }
    }
}
