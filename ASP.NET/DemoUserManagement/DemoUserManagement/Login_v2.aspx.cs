using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class Login_v2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static SessionModel LoginUser(string username, string password)
        {
            int authenticatedUserId = UserLogic.GetUserID(username, password);

            if (authenticatedUserId > 0)
            {
                bool isAdmin = UserLogic.IsAdmin(authenticatedUserId);
                SessionModel userSession = new SessionModel
                {
                    UserId = authenticatedUserId,
                    IsAdmin = isAdmin
                };

                Constants.SetSessionDetail(userSession);

                return userSession;
            }
            else
            {
                return new SessionModel { UserId = -1, IsAdmin = false };
            }
        }

    }
}