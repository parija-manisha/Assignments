using DemoUserManagement.Business;
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
    public partial class Login_v2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static int LoginUser(string username, string password)
        {
            int authenticatedUserId = UserLogic.GetUserID(username, password);
            if (authenticatedUserId > 0)
            {
                HttpContext.Current.Session["UserID"] = authenticatedUserId;
                return authenticatedUserId;
            }
            else
            {
                return -1;
            }
        }

    }
}