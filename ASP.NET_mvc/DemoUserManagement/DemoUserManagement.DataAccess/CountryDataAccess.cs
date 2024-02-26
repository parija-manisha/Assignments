using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccess
{
    public class CountryDataAccess
    {
        public static List<Country> GetCountry()
        {
            List<Country> countryList = new List<Country>();
            try
            {
                using (UserManagementTableEntities context = new UserManagementTableEntities())
                {
                    countryList = context.Countries.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not retrieve Country Details", ex);
            }
            return countryList;
        }

        public static int GetCountryIDByName(string countryName)
        {
            int countryID = 0;

            try
            {
                using (var context = new UserManagementTableEntities())
                {
                    Country country = context.Countries
                        .Where(c => c.CountryName == countryName)
                        .FirstOrDefault();

                    if (country != null)
                    {
                        countryID = country.CountryID;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError($"Error while retrieving CountryID for {countryName}", ex);
            }

            return countryID;
        }
    }
}
