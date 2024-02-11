using RegistrationFormASP.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistrationFormASP
{
    public partial class Branch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = Connection.Connect())
                if (!this.IsPostBack)
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
                    gridServiceBranch.DataSource = dt;
                    gridServiceBranch.DataBind();
                }

            }
        }
        private DataTable GetDataFromDatabase()
        {
            using (var connection = Connection.Connect())
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Branch", connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}