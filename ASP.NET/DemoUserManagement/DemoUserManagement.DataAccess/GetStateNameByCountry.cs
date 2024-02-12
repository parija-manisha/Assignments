using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
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
                using (UserManagementTableEntities2 context = new UserManagementTableEntities2())
                {
                    stateList = context.States.Where(states => states.CountryID == countryId).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Coulnot retrieve State Name By Countries", ex);
            }
            return stateList;
        }
    }
}
