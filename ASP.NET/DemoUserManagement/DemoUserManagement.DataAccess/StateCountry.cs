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
                    stateList = context.States.Where(s => s.CountryID == countryId).ToList();
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
