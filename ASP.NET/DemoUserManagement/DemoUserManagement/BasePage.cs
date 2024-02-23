using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace DemoUserManagement
{
    public class BasePage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var userSession = Constants.GetSessionDetail();

            if (HttpContext.Current.Request.Url.AbsolutePath.EndsWith("Login_v2.aspx", StringComparison.OrdinalIgnoreCase))
            {
            }

            if (HttpContext.Current.Request.Url.AbsolutePath.EndsWith("UserDetails_v2.aspx", StringComparison.OrdinalIgnoreCase))
            {

                if (Request.QueryString[Constants.ObjectIDName.UserID] != null && int.TryParse(Request.QueryString[Constants.ObjectIDName.UserID], out int userId))
                {
                    if (userSession != null)
                    {
                        if (userSession.UserId != userId && !userSession.IsAdmin)
                        {
                            Response.Redirect("UserDetails_v2.aspx?" + Constants.ObjectIDName.UserID + "=" + userSession.UserId);
                        }
                    }
                }
            }

        }

        //public void RedirectToSamePage(string targetURL)
        //{
        //    var currentPageName = Path.GetFileName(HttpContext.Current.Request.Url.AbsolutePath);

        //    if ((!currentPageName.Equals(Path.GetFileName(targetURL), StringComparison.OrdinalIgnoreCase)))
        //    {
        //        Response.Redirect(targetURL);
        //    }
        //}

        [WebMethod]
        public static object CheckUserAuthorisation(SessionModel userSession)
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
