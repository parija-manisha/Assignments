using DemoUserManagement.Business;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text;
            string password = txtPassword.Text;

            int authenticatedUserId = UserLogic.GetUserID(userName, password);

            if (authenticatedUserId > 0)
            {
                Session["UserID"] = authenticatedUserId;
                Response.Redirect($"UserDetails_v2.aspx?{Constants.ObjectIDName.UserID}={authenticatedUserId}");
            }

            else
            {
                lblMessage.Text = "Invalid UserName or Password";
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserDetails_v2.aspx");
        }
    }
}