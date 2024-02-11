using RegistrationFormASP.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistrationFormASP
{
    public partial class NewBranch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BranchButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                string name = BranchText.Text;

                if (string.IsNullOrEmpty(name))
                {
                    throw new Exception("Please fill in all the required fields.");
                }

                using (var con = Connection.Connect())
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Branch WHERE [Branch Name] = @name";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@name", name);
                        int branchCount = (int)checkCmd.ExecuteScalar();

                        if (branchCount > 0)
                        {
                            throw new Exception("Branch already exists.");
                        }
                    }

                    string insertQuery = "INSERT INTO Branch VALUES (@name)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.ExecuteNonQuery();
                    }

                }
               
                ErrorMessage.Text = "Branch Added Successfully";
                
                if (flag)
                {
                    BranchText.Text = "";

                    Server.Transfer("Branch.aspx");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
            }
        }
    }
}