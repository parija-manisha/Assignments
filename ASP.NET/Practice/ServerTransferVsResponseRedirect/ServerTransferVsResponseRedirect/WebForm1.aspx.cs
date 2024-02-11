using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServerTransferVsResponseRedirect
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ServerTransfer_Click(object sender, EventArgs e)
        {
            Server.Transfer("ServerTransfer.aspx");
        }

        protected void ResponseRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResponseRedirect.aspx");
        }
    }
}