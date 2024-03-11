using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
using Microsoft.Extensions.Logging;
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
            try
            {
                List<Aircraft> aircraftList = new List<Aircraft>();
                using (var context = new AirportFuelInventoryEntities())
                {
                    aircraftList = context.Aircraft
                        .OrderBy(t => t.Aircraft_Id)
                        .ToList();
                }

                return aircraftList;
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch aircraft list", ex);
                return null;
            }
        }

        public static List<Aircraft> GetAircraftNameList()
        {
            try
            {
                List<Aircraft> aircraftNameList;
                using (var context = new AirportFuelInventoryEntities())
                {
                    var anonymousList = context.Aircraft
                        .Select(a => new
                        {
                            a.Aircraft_Id,
                            a.Aircraft_Name
                        })
                        .ToList();

                    aircraftNameList = anonymousList.Select(a => new Aircraft
                    {
                        Aircraft_Id = a.Aircraft_Id,
                        Aircraft_Name = a.Aircraft_Name
                    })
                    .ToList();
                }
                return aircraftNameList;
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch aircraft name list", ex);
                return null;
            }
        }


        public static bool NewAircraft(AircraftDTO aircraftDTO)
        {
            try
            {
                using (var context = new AirportFuelInventoryEntities())
                {
                    Aircraft aircraft = new Aircraft
                    {
                        Aircraft_Name = aircraftDTO.Aircraft_Name,
                        Airline = aircraftDTO.Airline,
                        Source = aircraftDTO.Source,
                        Destination = aircraftDTO.Destination,
                    };
                    context.Aircraft.Add(aircraft);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch aircraft list", ex);
                return false;
            }
        }
    }
}
