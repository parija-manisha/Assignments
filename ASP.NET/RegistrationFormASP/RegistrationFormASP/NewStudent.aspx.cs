using RegistrationFormASP.Util;
using System.Data.SqlClient;
using System;
using RegistrationFormASP.Business;
using System.Configuration;

namespace RegistrationFormASP
{
    public partial class NewStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Email"] != null)
                {
                    ErrorMessage.Text = "This email ID already exists";
                }
            ErrorMessage.Text = "";
            }
        }

        protected void StudentButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                string name = NameText.Text;
                string gender = GenderText.Text;
                string email = EmailText.Text;
                string phone = PhoneText.Text;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
                {
                    throw new Exception("Please fill in all the required fields.");
                }

                if (Session["Email"] != null && Session["Email"].ToString() == email)
                {
                    ErrorMessage.Text = "This email ID already exists in the session";
                    return; 
                }

                using (var con = Connection.Connect())
                {
                    con.Open();

                    string query = "INSERT INTO Students VALUES (@name, @phone, @email, @gender)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phone", phone);

                        cmd.ExecuteNonQuery();
                    }
                }
                Session["Email"] = email;
                flag = true;

                if (flag)
                {
                    NameText.Text = "";
                    GenderText.Text = "";
                    EmailText.Text = "";
                    PhoneText.Text = "";

                    Email.SendEmail(email);

                    Server.Transfer("Student.aspx");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
            }
        }
    }
}
