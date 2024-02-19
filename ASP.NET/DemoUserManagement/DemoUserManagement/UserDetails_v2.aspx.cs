using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class UserDetails_v2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //[WebMethod]
        //public static bool IsEmailExists(string email)
        //{
        //    return UserLogic.IsEmailExists(email);
        //}

        [WebMethod]
        public static int SaveUser(UserDetailDTO user, List<AddressDetailDTO> addresses)
        {
            try
            {
                int userId = UserLogic.SaveUser(user);

                foreach (var address in addresses)
                {
                    address.UserID = userId;
                    UserLogic.SaveAddress(address);
                }

                if (userId != -1)
                {
                    UserLogic.SaveRole(userId);
                    if (UserLogic.IsAdmin(userId))
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Registration Failed", ex);
                return 0;
            }

            return -1;
        }

        [WebMethod]
        public static List<CountryDTO> GetCountries()
        {
            List<CountryDTO> countries = CountryLogic.GetCountryList();
            return countries;
        }

        [WebMethod]
        public static List<StateDTO> GetStates(int countryId)
        {
            List<StateDTO> states = StateLogic.GetStateList(countryId);
            return states;
        }
    }
}