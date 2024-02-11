using RegistrationFormASP.Util;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace RegistrationFormASP
{
    public partial class Student : System.Web.UI.Page
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
                    gridServiceStudent.DataSource = dt;
                    gridServiceStudent.DataBind();
                }
            }
        }

        private DataTable GetDataFromDatabase()
        {
            using (var connection = Connection.Connect())
            {
                using (var command = new SqlCommand("SELECT * FROM Students", connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        protected void gridServiceStudent_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = GetDataFromDatabase();
            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gridServiceStudent.DataSource = dt;
                gridServiceStudent.DataBind();
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

        protected void gridServiceStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridServiceStudent.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}
