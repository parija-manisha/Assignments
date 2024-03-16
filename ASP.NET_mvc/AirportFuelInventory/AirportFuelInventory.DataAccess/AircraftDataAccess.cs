using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.DataAccess
{
    public class AircraftDataAccess
    {
        public static List<AircraftDTO> GetAircraftList(int start, int length)
        {
            try
            {
                List<AircraftDTO> aircraftList = new List<AircraftDTO>();
                using (var context = new AirportFuelInventoryEntities())
                {
                    var aircraftEntities = context.Aircraft
                        .OrderBy(t => t.Aircraft_id)
                        .Skip(start)
                        .Take(length)
                        .ToList();

                    aircraftList = aircraftEntities.Select(aircraft => new AircraftDTO
                    {
                        Aircraft_id = aircraft.Aircraft_id,
                        Aircraft_no = aircraft.Aircraft_no,
                        Airline = aircraft.Airline,
                        Source_id = aircraft.Source_id,
                        Destination_id = aircraft.Destination_id,
                        Sources = aircraft.Source != null ? new List<SourceDTO> {
                            new SourceDTO {
                                Source_id = aircraft.Source.Source_id,
                                Source_name = aircraft.Source.Source_name
                            }
                        } : null,
                        Destinations = aircraft.Destination != null ? new List<DestinationDTO> {
                            new DestinationDTO {
                                Destination_id = aircraft.Destination.Destination_id,
                                Destination_name = aircraft.Destination.Destination_name
                            }
                        } : null
                    }).ToList();
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
                            a.Aircraft_id,
                            a.Aircraft_no
                        })
                        .ToList();

                    aircraftNameList = anonymousList.Select(a => new Aircraft
                    {
                        Aircraft_id = a.Aircraft_id,
                        Aircraft_no = a.Aircraft_no
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

        public static double GetTotalRecords()
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                return context.Aircraft.Count();
            }
        }

        public static bool NewAircraft(AircraftDTO aircraftDTO)
        {
            try
            {
                using (var context = new AirportFuelInventoryEntities())
                {
                    if (aircraftDTO.Aircraft_id != 0)
                    {
                        UpdateAircraft(aircraftDTO.Aircraft_id, aircraftDTO);
                        return true;
                    }
                    Aircraft aircraft = new Aircraft
                    {
                        Aircraft_no = aircraftDTO.Aircraft_no,
                        Airline = aircraftDTO.Airline,
                        Source_id = aircraftDTO.Source_id,
                        Destination_id = aircraftDTO.Destination_id,
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

        public static AircraftDTO GetAircraftDetailById(int aircraftId)
        {
            try
            {
                using (AirportFuelInventoryEntities context = new AirportFuelInventoryEntities())
                {
                    var aircraftDetails = context.Aircraft
                        .FirstOrDefault(u => u.Aircraft_id == aircraftId);

                    if (aircraftDetails != null)
                    {
                        var aircraft = new AircraftDTO
                        {
                            Aircraft_id = aircraftId,
                            Aircraft_no = aircraftDetails.Aircraft_no,
                            Airline = aircraftDetails.Airline,
                            Source_id = aircraftDetails.Source_id,
                            Destination_id = aircraftDetails.Destination_id,
                            Sources = SourceDataAccess.GetSourceList().Select(source => new SourceDTO
                            {
                                Source_id = source.Source_id,
                                Source_name = source.Source_name,
                            }).ToList(),
                            Destinations = DestinationDataAccess.GetDestinationList().Select(destination => new DestinationDTO
                            {
                                Destination_id = destination.Destination_id,
                                Destination_name = destination.Destination_name,
                            }).ToList()
                        };

                        return aircraft;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch Aircraft details", ex);
                return null;
            }
        }

        public static void UpdateAircraft(int aircraftId, AircraftDTO aircraftDTO)
        {
            using (AirportFuelInventoryEntities context = new AirportFuelInventoryEntities())
            {
                Aircraft aircraft = context.Aircraft.Find(aircraftId);
                if (aircraft != null)
                {
                    aircraft.Aircraft_no = aircraftDTO.Aircraft_no;
                    aircraft.Airline = aircraftDTO.Airline;
                    aircraft.Destination_id = aircraftDTO.Destination_id;
                    aircraft.Source_id = aircraftDTO.Source_id;
                }
                context.SaveChanges();
            }
        }
    }
}
