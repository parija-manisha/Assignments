using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridView
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Name"), new DataColumn("Country") });
                ViewState["Customers"] = dt;
                this.BindGrid();
            }
        }

        protected void BindGrid()
        {
            GridView1.DataSource = (DataTable)ViewState["Customers"];
            GridView1.DataBind();
        }

        protected void Insert(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["Customers"];
            dt.Rows.Add(txtName.Text.Trim(), txtCountry.Text.Trim());
            ViewState["Customers"] = dt;
            this.BindGrid();
            txtName.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }
    }
}