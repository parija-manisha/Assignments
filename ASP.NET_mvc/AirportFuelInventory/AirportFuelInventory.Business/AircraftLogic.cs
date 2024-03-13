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
        public static List<AircraftDTO> GetAircraftList(int start, int length)
        {
            List<AircraftDTO> aircrafts = AircraftDataAccess.GetAircraftList(start, length);

            return aircrafts;
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
                Aircraft_no = aircraft.Aircraft_no,
                Aircraft_id = aircraft.Aircraft_id,
            }).ToList();

            return aircraftDTOs;
        }

        public static double GetTotalRecords()
        {
            return AircraftDataAccess.GetTotalRecords();
        }

        public static AircraftDTO GetAircraftDetailById(int aircraftId)
        {
            return AircraftDataAccess.GetAircraftDetailById(aircraftId);
        }
    }
}
