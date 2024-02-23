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
    public class StateCountry
    {

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
                            s.StateName
                        })
                        .AsEnumerable()
                        .Select(s => new State
                        {
                            StateID = s.StateID,
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



    }
}
