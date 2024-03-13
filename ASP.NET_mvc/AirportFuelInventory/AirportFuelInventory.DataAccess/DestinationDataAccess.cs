using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //public static int GetCountryIDByName(string countryName)
        //{
        //    int countryID = 0;

        //    try
        //    {
        //        using (var context = new UserManagementTableEntities())
        //        {
        //            Country country = context.Countries
        //                .Where(c => c.CountryName == countryName)
        //                .FirstOrDefault();

        //            if (country != null)
        //            {
        //                countryID = country.CountryID;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.AddError($"Error while retrieving CountryID for {countryName}", ex);
        //    }

        //    return countryID;
        //}
    }
}
