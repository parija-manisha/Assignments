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
    public partial class UpdateStudent : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void UpdateStudentButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool flag = false;
        //        string id = IDText.Text;
        //        string name = "";
        //        string gender = "";
        //        string email = "";
        //        string phone = "";

        //        using (var con = Connection.Connect())
        //        {
        //            con.Open();

        //            string query = "INSERT INTO Students VALUES (@name, @phone, @email, @gender)";
        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                cmd.Parameters.AddWithValue("@name", name);
        //                cmd.Parameters.AddWithValue("@gender", gender);
        //                cmd.Parameters.AddWithValue("@email", email);
        //                cmd.Parameters.AddWithValue("@phone", phone);

        //                cmd.ExecuteNonQuery();
        //            }

        //        }
        //        ErrorMessage.Text = "Student Added Successfully";
        //        flag = true;
        //        if (flag)
        //        {
        //            NameText.Text = "";
        //            GenderText.Text = "";
        //            EmailText.Text = "";
        //            PhoneText.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage.Text = ex.Message;
        //    }
        //}
    }
}