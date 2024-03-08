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
    public class AircraftLogic
    {
        public static List<AircraftDTO> GetAircraftList()
        {
            List<Aircraft> aircrafts = AircraftDataAccess.GetAircraftList();
            List<AircraftDTO> aircraftList = aircrafts.Select(aircraft => new AircraftDTO
            {
                Aircraft_Name=aircraft.Aircraft_Name,
                Airline=aircraft.Airline,
                Source=aircraft.Source,
                Destination=aircraft.Destination,
            }).ToList();

            return aircraftList;
        }

        public static void NewAircraft(AircraftDTO aircraft)
        {
            AircraftDataAccess.NewAircraft(aircraft);
        }
    }
}
