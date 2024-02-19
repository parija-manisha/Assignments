using DemoUserManagement.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                SiteName.Text = Page.Title;
                bool isLoginPage = Request.Url.AbsolutePath.ToLower().EndsWith("login.aspx");

                bool isLoggedIn = Session["UserID"] != null;
                bool isAdmin = isLoggedIn && UserLogic.IsAdmin(Convert.ToInt32(Session["UserID"]));

                SetNavbarVisibility(isLoginPage, isLoggedIn, isAdmin);

            //}
        }

        private void SetNavbarVisibility(bool isLoginPage, bool isLoggedIn, bool isAdmin)
        {
            UserDetailsLink.Visible = isLoggedIn;

            if (isAdmin)
            {
                UpdateUserLink.Visible = true;
                UserDetailsLink.Visible = true;
            }
            else
            {
                UpdateUserLink.Visible = false;
                UserDetailsLink.Visible = false;
            }

            if (isLoginPage)
            {
                AddLogoutButton();
            }
            else
            {
                AddLogoutButton();
            }
        }


        private void AddLogoutButton()
        {
            HyperLink logoutLink = new HyperLink();
            logoutLink.CssClass = "nav-link";
            logoutLink.Text = "Logout";
            logoutLink.NavigateUrl = "~/Logout.aspx";
            navBarLinks.Controls.Add(new LiteralControl("<li class='nav-item'>"));
            navBarLinks.Controls.Add(logoutLink);
            navBarLinks.Controls.Add(new LiteralControl("</li>"));
        }
    }
}