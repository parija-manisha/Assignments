using StudentDetailsWebForms.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentDetailsWebForms
{
    public partial class NewStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void StudentButton_Click(object sender, EventArgs e)
        {
            try
            {
                string name = NameText.Text;
                string gender = GenderText.Text;
                string dateOfBirth = DateOfBirthText.Text;
                string phone = PhoneText.Text;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(dateOfBirth) || string.IsNullOrEmpty(phone))
                {
                    throw new Exception("Please fill in all the required fields.");
                }

                using (var con = Connection.Connect())
                {
                    con.Open();

                    string query = "INSERT INTO Student VALUES (@name, @gender, @dateOfBirth, @phone)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                        cmd.Parameters.AddWithValue("@phone", phone);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
                ErrorMessage.Text = ex.Message;
            }
        }
    }
}