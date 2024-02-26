using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccess
{
    public class StateDataAccess
    {
        public static List<State> GetState()
        {
            List<State> stateList = new List<State>();
            try
            {
                using (UserManagementTableEntities context = new UserManagementTableEntities())
                {
                    stateList = context.States.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Couldnot retrieve State Details", ex);
            }
            return stateList;
        }

        public static List<State> GetStateByCountry(int countryId)
        {
            List<State> stateList = new List<State>();
            try
            {
                using (UserManagementTableEntities context = new UserManagementTableEntities())
                {
                    var states = context.States
                        .Where(s => s.CountryID == countryId)
                        .Select(s => new
                        {
                            s.StateID,
                            s.StateName,
                            s.CountryID
                        })
                        .AsEnumerable()
                        .Select(s => new State
                        {
                            StateID = s.StateID,
                            CountryID = s.CountryID,
                            StateName = s.StateName
                        })
                        .ToList();

                    stateList.AddRange(states);
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not retrieve State Name By Countries", ex);
            }
            return stateList;
        }

        public static int GetStateIDByName(string stateName)
        {
            int stateID = 0;

            using (var connection = Connection.Connect())
            {
                string query = "SELECT StateID FROM State WHERE StateName = @stateName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@stateName", stateName);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            stateID = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.AddError($"Error while retrieving CountryID for {stateName}", ex);
                    }
                }
            }

            return stateID;
        }
    }
}
