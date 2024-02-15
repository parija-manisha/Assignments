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
                Logger.AddError("Couldnot retrive Country Details", ex);
            }
            return countryList;
        }

        public static int GetCountryIDByName(string countryName)
        {
            int countryID = 0;

            using (var connection = Connection.Connect())
            {
                string query = "SELECT CountryID FROM Country WHERE CountryName = @countryName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryName", countryName);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            countryID = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.AddError($"Error while retrieving CountryID for {countryName}", ex);
                    }
                }
            }

            return countryID;
        }
    }
}
