using RegistrationFormASP.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace RegistrationFormASP
{
    public partial class DeleteStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //protected void DeleteStudentButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool flag = false;
        //        string stdId = DeleteIdText.Text;
        //        string name = "";
        //        string gender = "";
        //        string email = "";
        //        string phone = "";

        //        if (string.IsNullOrEmpty(stdId))
        //        {
        //            throw new Exception("Please fill in the required fields.");
        //        }

        //        using (var con = Connection.Connect())
        //        {
        //            con.Open();

        //            string retrieveQuery = "SELECT [Student Name],[Phone Number] ,[Email] ,[Gender] FROM Students WHERE [Student ID] = @stdId";
        //            using (SqlCommand retrieveCmd = new SqlCommand(retrieveQuery, con))
        //            {
        //                retrieveCmd.Parameters.AddWithValue("@stdId", stdId);
        //                SqlDataReader reader = retrieveCmd.ExecuteReader();

        //                if (reader.Read())
        //                {
        //                    name = reader["Student Name"].ToString();
        //                    gender = reader["Phone Number"].ToString();
        //                    email = reader["Email"].ToString();
        //                    phone = reader["Gender"].ToString();
        //                }
        //                else
        //                {
        //                    throw new Exception("Student not found for the provided Student ID.");
        //                }

        //                reader.Close();
        //            }

        //            string deleteQueryReference = "DELETE FROM Enrollment WHERE [STUDENT ID] = @stdId";
        //            using (SqlCommand deleteCmd = new SqlCommand(deleteQueryReference, con))
        //            {
        //                deleteCmd.Parameters.AddWithValue("@stdId", stdId);
        //                deleteCmd.ExecuteNonQuery();
        //            }
        //            string deleteQuery = "DELETE FROM Students WHERE [Student ID] = @stdId";
        //            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
        //            {
        //                deleteCmd.Parameters.AddWithValue("@stdId", stdId);
        //                deleteCmd.ExecuteNonQuery();
        //            }
        //        }

        //        DeleteNameText.Text = name;
        //        DeleteGenderText.Text = gender;
        //        DeleteEmailText.Text = email;
        //        DeletePhoneText.Text = phone;

        //        DeleteErrorMessage.Text = "Student Deleted Successfully";
        //        flag = true;

        //        if (flag)
        //        {
        //            DeleteIdText.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DeleteErrorMessage.Text = ex.Message;
        //    }
        //}
    }
}