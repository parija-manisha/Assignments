using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportFuelInventory.DataAccess
{
    public class DestinationDataAccess
    {
        public static List<Destination> GetDestinationList()
        {
            List<Destination> destinationList = new List<Destination>();
            try
            {
                using (AirportFuelInventoryEntities context = new AirportFuelInventoryEntities())
                {
                    destinationList = context.Destinations.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not retrieve Destination Details", ex);
            }
            return destinationList;
        }
    }
}
