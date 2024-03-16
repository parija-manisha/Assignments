using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.DataAccess
{
    public class AirportDataAccess
    {
        public static List<Airport> GetAirportList(int start, int length)
        {
            try
            {
                List<Airport> airportList = new List<Airport>();
                using (var context = new AirportFuelInventoryEntities())
                {
                    airportList = context.Airports
                        .OrderBy(t => t.Airport_name)
                        .Skip(start)
                        .Take(length)
                        .ToList();
                }

                return airportList;
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch airport list", ex);
                return null;
            }
        }

        public static List<Airport> GetAirportNameList()
        {
            try
            {
                List<Airport> airportNameList;
                using (var context = new AirportFuelInventoryEntities())
                {
                    var anonymousList = context.Airports
                        .Select(a => new
                        {
                            a.Airport_id,
                            a.Airport_name
                        })
                        .ToList();

                    airportNameList = anonymousList.Select(a => new Airport
                    {
                        Airport_id = a.Airport_id,
                        Airport_name = a.Airport_name
                    })
                    .ToList();
                }
                return airportNameList;
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch airport name list", ex);
                return null;
            }
        }

        public static bool NewAirport(AirportDTO airportDto)
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                Airport airport = new Airport
                {
                    Airport_name = airportDto.Airport_name,
                    Fuel_Capacity = airportDto.Fuel_Capacity,
                };
                context.Airports.Add(airport);
                context.SaveChanges();
                return true;
            }
        }

        public static List<ReportSummary.AirportSummary> GetAvailableFuel(int start, int length)
        {
            try
            {
                using (var context = new AirportFuelInventoryEntities())
                {
                    var transactions = context.Transactions
                        .Where(t => t.Transaction_type == (int)TransactionType.In || t.Transaction_type == (int)TransactionType.Out)
                        .GroupBy(t => t.Airport_id)
                        .Select(group => new
                        {
                            AirportId = group.Key,
                            TotalQuantity = group.Sum(t => t.Quantity * (t.Transaction_type == (int)TransactionType.In ? 1 : -1))
                        })
                        .ToList();

                    var airports = context.Airports.ToDictionary(a => a.Airport_id, a => a);

                    var result = new List<ReportSummary.AirportSummary>();

                    var paginatedAirports = airports.Skip(start)
                                          .Take(length)
                                          .ToList();

                    foreach (var airport in paginatedAirports)
                    {
                        var airportId = airport.Key;
                        var airportName = airport.Value.Airport_name;

                        var transaction = transactions.FirstOrDefault(t => t.AirportId == airportId);

                        var availableFuel = transaction != null
                            ? ((airport.Value.Fuel_Capacity + transaction.TotalQuantity) < 0)
                                ? 0
                                : airport.Value.Fuel_Capacity + transaction.TotalQuantity
                            : airport.Value.Fuel_Capacity;

                        result.Add(new ReportSummary.AirportSummary
                        {
                            Airport_id = airportId,
                            Airport_name = airportName,
                            AvailableFuel = availableFuel
                        });
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch available fuel report", ex);
                return null;
            }
        }

        public static List<ReportSummary.FuelSummary> GetFuelConsumptionReport(int start, int length)
        {
            try
            {
                using (var context = new AirportFuelInventoryEntities())
                {
                    var transactions = context.Transactions
                        .Where(t => t.Transaction_type == (int)TransactionType.In || t.Transaction_type == (int)TransactionType.Out)
                        .GroupBy(t => t.Airport_id)
                        .Select(group => new
                        {
                            AirportId = group.Key,
                            TotalQuantity = group.Sum(t => t.Quantity * (t.Transaction_type == (int)TransactionType.In ? 1 : -1))
                        })
                        .ToList();

                    var airports = context.Airports.ToDictionary(a => a.Airport_id, a => a);
                    var aircrafts = context.Aircraft.ToDictionary(a => a.Aircraft_id, a => a);

                    var transactionsList = context.Transactions
                        .GroupBy(t => t.Airport_id)
                        .ToDictionary(group => group.Key, group => group.ToList());

                    var result = transactions
                        .Skip(start)
                        .Take(length)
                        .Select(t => new ReportSummary.FuelSummary
                        {
                            AirportName = airports[t.AirportId].Airport_name,
                            TransactionDTO = transactionsList[t.AirportId]
                                .Select(tr => new TransactionDTO
                                {
                                    Transaction_date_time = tr.Transaction_date_time,
                                    Quantity = tr.Quantity,
                                    Transaction_type = tr.Transaction_type,
                                    Aircraft_id = tr.Aircraft_id,
                                    AircraftDTOs = tr.Airport != null ? new List<AircraftDTO> {
                            new AircraftDTO {
                                Aircraft_no=tr.Aircraft.Aircraft_no,
                                Aircraft_id = tr.Aircraft_id
                            }
                        } : null,
                                })
                                .ToList(),
                            AvailableFuel = airports[t.AirportId].Fuel_Capacity - t.TotalQuantity
                        })
                        .ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch fuel consumption report", ex);
                return null;
            }
        }

        public static double GetTotalRecords()
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                var count = context.Airports.Count();
                return count;
            }
        }

        public static AirportDTO GetAirportDetailById(int airportId)
        {
            try
            {
                using (AirportFuelInventoryEntities context = new AirportFuelInventoryEntities())
                {
                    var airportDetails = context.Airports
                        .FirstOrDefault(u => u.Airport_id == airportId);

                    if (airportDetails != null)
                    {
                        var airport = new AirportDTO
                        {
                            Airport_id = airportId,
                            Airport_name = airportDetails.Airport_name,
                            Fuel_Capacity = airportDetails.Fuel_Capacity,
                        };

                        return airport;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not fetch airport details", ex);
                return null;
            }
        }

    }
}
