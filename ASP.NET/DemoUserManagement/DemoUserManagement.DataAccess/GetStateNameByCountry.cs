using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccess
{
    public class GetStateNameByCountry
    {

        public static List<State> StateCountry(int countryId)
        {
            List<State> stateList = new List<State>();
            try
            {
                using (var connection = Connection.Connect())
                {
                    connection.Open();

                    string query = "SELECT StateName FROM States WHERE CountryID = @CountryID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryID", countryId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                State state = new State
                                {
                                    StateName = reader["StateName"].ToString(),
                                };

                                stateList.Add(state);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not retrieve State Name By Countries", ex);
            }
            return stateList;
        }
    }
}
