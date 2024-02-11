using RegistrationFormASP.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace RegistrationFormASP
{
    public partial class NewEnrollment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearForm();
            }
        }


        //protected void EnrollmentBranchText_Input(object sender, EventArgs e)
        //{
        //    if (int.TryParse(EnrollmentBranchText.Text, out int branchId))
        //    {
        //        using (var con = Connection.Connect())
        //        {
        //            con.Open();
        //            string retrieveBranchQuery = "SELECT [Branch Name] FROM Branch WHERE [Branch ID] = @branchId";
        //            using (SqlCommand retrieveBranchCmd = new SqlCommand(retrieveBranchQuery, con))
        //            {
        //                retrieveBranchCmd.Parameters.AddWithValue("@branchId", branchId);
        //                object result = retrieveBranchCmd.ExecuteScalar();

        //                if (result != null)
        //                {
        //                    string branchName = result.ToString();
        //                    EnrollmentBranchNameText.Text = branchName;
        //                    EnrollmentBranchNameText.Enabled = false;
        //                }
        //                else
        //                {
        //                    ErrorMessage.Text = "Branch not found for the provided BranchID.";
        //                    ClearForm();
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ErrorMessage.Text = "Invalid Branch ID.";
        //        ClearForm();
        //    }
        //}

        protected void EnrollmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                string stdID = EnrollmentStudentText.Text;
                string branchID = EnrollmentBranchText.Text;

                if (string.IsNullOrEmpty(stdID) || string.IsNullOrEmpty(branchID))
                {
                    ErrorMessage.Text = "Please fill in all the required fields.";
                }

                using (var con = Connection.Connect())
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Enrollment WHERE [Student ID] = @stdid";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@stdid", stdID);
                        int enrollCount = (int)checkCmd.ExecuteScalar();

                        if (enrollCount > 0)
                        {
                            throw new Exception("Enrollment already exists.");
                        }
                    }

                    string insertQuery = "INSERT INTO Enrollment VALUES (@stdId, @branchId, @branchName)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@stdId", stdID);
                        cmd.Parameters.AddWithValue("@branchId", branchID);
                        cmd.Parameters.AddWithValue("@branchName", EnrollmentBranchNameText.Text);
                        cmd.ExecuteNonQuery();
                    }

                }

                ErrorMessage.Text = "Enrollemnt Successful";

                Server.Transfer("Enrollment.aspx");

                ClearForm();
            }

            catch (System.Threading.ThreadAbortException)
            {
            }

            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
            }
        }

        private void ClearForm()
        {
            EnrollmentStudentText.Text = "";
            EnrollmentBranchText.Text = "";
            EnrollmentBranchNameText.Text = "";
            EnrollmentBranchNameText.Enabled = true;
        }

            //EnrollmentBranchText.Attributes["oninput"] = "enrollmentBranchTextInput()";
    }
}