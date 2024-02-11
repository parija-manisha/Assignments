using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PageLifeCycle
{
    public partial class _Default : Page
    {
       protected void Page_Init(object sender, EventArgs e)
        {
            // This code runs during the page initialization
            txtInput.Text = "Initialized in Page_Init";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // This code runs only on the initial page load, not during postbacks
                lblMessage.Text = "Welcome to the Page Life Cycle Example!";
            }

            // This code runs on every page load, including postbacks
            lblMessage.Text += "<br />Page_Load executed";
        }

        protected void btnClickMe_Click(object sender, EventArgs e)
        {
            // This code runs when the button is clicked
            lblMessage.Text = "Button Clicked! Text: " + txtInput.Text;
        }

        protected void chkToggle_CheckedChanged(object sender, EventArgs e)
        {
            // This code runs when the checkbox is checked/unchecked
            if (chkToggle.Checked)
            {
                txtInput.Visible = false;
                btnClickMe.Enabled = false;
                lblMessage.Text = "Controls hidden.";
            }
            else
            {
                txtInput.Visible = true;
                btnClickMe.Enabled = true;
                lblMessage.Text = "Controls visible.";
            }
        }
    }
}