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
                Aircraft_Name = aircraft.Aircraft_Name,
                Airline = aircraft.Airline,
                Source = aircraft.Source,
                Destination = aircraft.Destination,
            }).ToList();

            return aircraftList;
        }

        public static bool NewAircraft(AircraftDTO aircraft)
        {
            return AircraftDataAccess.NewAircraft(aircraft);
        }

        public static List<AircraftDTO> GetAircraftNameList()
        {
            List<Aircraft> aircraftName = AircraftDataAccess.GetAircraftNameList();
            List<AircraftDTO> aircraftDTOs = aircraftName.Select(aircraft => new AircraftDTO
            {
                Aircraft_Name = aircraft.Aircraft_Name,
                Aircraft_Id = aircraft.Aircraft_Id,
            }).ToList();

            return aircraftDTOs;
        }
    }
}
