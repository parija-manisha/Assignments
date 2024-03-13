using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.DataAccess
{
    public class SourceDataAccess
    {
        public static List<Source> GetSourceList()
        {
            List<Source> sourceList = new List<Source>();
            try
            {
                using (AirportFuelInventoryEntities context = new AirportFuelInventoryEntities())
                {
                    sourceList = context.Sources.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not retrieve Country Details", ex);
            }
            return sourceList;
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
