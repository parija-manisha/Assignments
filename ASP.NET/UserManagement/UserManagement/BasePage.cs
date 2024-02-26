using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UserManagement.Models;

namespace UserManagement
{
    public class BasePage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static object CheckAuthorization(UserSession userSession)
        {
            try
            {
                bool isLoggedIn = (userSession != null && userSession.UserId != -1);
                bool isAdmin = (isLoggedIn && userSession.IsAdmin);

                return new { IsLoggedIn = isLoggedIn, IsAdmin = isAdmin };
            }
            catch
            {
                return null;
            }
        }
    }
}