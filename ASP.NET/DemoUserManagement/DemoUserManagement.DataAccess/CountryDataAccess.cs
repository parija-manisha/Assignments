using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
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
                using (UserManagementTableEntities2 context = new UserManagementTableEntities2())
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
    }
}
