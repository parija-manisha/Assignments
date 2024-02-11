using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            using (var context = Connection.Connect())
            {
                DataTable dt = GetDataFromDatabase();

                if (dt != null && dt.Rows.Count > 0)
                {
                    GridViewUsers.DataSource = dt;
                    GridViewUsers.DataBind();
                }
            }
        }

        private DataTable GetDataFromDatabase()
        {
            using (var connection = Connection.Connect())
            {
                using (var command = new SqlCommand("SELECT * FROM UserDetails", connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        protected void GridViewUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = GetDataFromDatabase();
            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                GridViewUsers.DataSource = dt;
                GridViewUsers.DataBind();
            }
        }

        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string lastColumnSorted = ViewState["SortExpression"] as string;

            if (!string.IsNullOrEmpty(lastColumnSorted))
            {
                if (lastColumnSorted == column)
                {
                    sortDirection = ViewState["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
                }
            }

            ViewState["SortExpression"] = column;
            ViewState["SortDirection"] = sortDirection;

            return sortDirection;
        }

        protected void GridViewUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsers.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}