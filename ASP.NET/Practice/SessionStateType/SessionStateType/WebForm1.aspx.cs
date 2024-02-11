using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SessionStateType
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the name is already in session
                if (Session["UserName"] != null)
                {
                    string userName = Session["UserName"].ToString();
                    lblMessage.Text = $"Welcome back, {userName}!";
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Store the name in session
            Session["UserName"] = txtName.Text;
            lblMessage.Text = $"Hello, {txtName.Text}! Your name is stored in the session.";
        }
    }
}