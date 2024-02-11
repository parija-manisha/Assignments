using DemoUserManagement.Business;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class UserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Phone"] != null)
                {
                    ErrorMessage.Text = "This User already exists";
                }
                ErrorMessage.Text = "";
                if (Session["UserId"] != null)
                {
                    AddNote.Visible = true;
                }
                else
                {
                    AddNote.Visible = false;
                }
            }

            if (!IsPostBack)
            {
                PopulateCountryDropdown(PermanentCountry);
                PopulateStateDropdown(PermanentState, PermanentCountry.SelectedItem.Text);

                PopulateCountryDropdown(PresentCountry);
                PopulateStateDropdown(PresentState, PresentCountry.SelectedItem.Text);
            }
        }

        private void PopulateCountryDropdown(DropDownList ddl)
        {
            List<string> countries = GetCountriesFromDatabase();

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("", ""));
            foreach (string country in countries)
            {
                ddl.Items.Add(new ListItem(country, country));
            }
        }


        private void PopulateStateDropdown(DropDownList ddl, string selectedCountry)
        {
            List<string> states = GetStatesFromDatabase(selectedCountry);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("", ""));

            foreach (string state in states)
            {
                ddl.Items.Add(new ListItem(state, state));
            }
        }

        private List<string> GetCountriesFromDatabase()
        {
            List<string> countries = new List<string>();

            string query = "SELECT CountryName FROM Country";

            using (var connection = Connection.Connect())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string countryName = reader.GetString(0);
                            countries.Add(countryName);
                        }
                    }
                }
            }

            return countries;
        }

        private List<string> GetStatesFromDatabase(string selectedCountry)
        {
            List<string> states = new List<string>();

            string query = "SELECT StateName FROM State WHERE CountryName = @SelectedCountry";

            using (var connection =Connection.Connect())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SelectedCountry", selectedCountry);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string stateName = reader.GetString(0);
                            states.Add(stateName);
                        }
                    }
                }
            }

            return states;
        }

        protected void SaveUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                string firstName = TxtFirstName.Text;
                string middleName = TxtMiddleName.Text;
                string lastName = TxtLastName.Text;
                string gender = TxtGender.Text;
                string email = TxtEmailID.Text;
                string phone = TxtPhoneNumber.Text;
                string dateOfBirth = TxtDateOfBirth.Text;
                string fatherName = TxtFatherName.Text;
                string motherName = TxtMotherName.Text;

                string permanentCountry = PermanentCountry.SelectedItem.Text;
                string permanentState = PermanentState.SelectedItem.Text;
                string permanentCity = PermanentCity.Text;
                string permanentPincode = PermanentPincode.Text;
                string permanentAddressLine = PermanentAddressLine.Text;

                string presentCountry = PresentCountry.SelectedItem.Text;
                string presentState = PresentState.SelectedItem.Text;
                string presentCity = PresentCity.Text;
                string presentPincode = PresentPincode.Text;
                string presentAddressLine = PresentAddressLine.Text;

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(gender)
                    || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(dateOfBirth)
                    || string.IsNullOrEmpty(fatherName) || string.IsNullOrEmpty(motherName)
                    || string.IsNullOrEmpty(permanentCountry) || string.IsNullOrEmpty(permanentState) || string.IsNullOrEmpty(permanentCity) || string.IsNullOrEmpty(permanentPincode) || string.IsNullOrEmpty(permanentAddressLine)
                    || string.IsNullOrEmpty(presentCountry) || string.IsNullOrEmpty(presentState) || string.IsNullOrEmpty(presentCity) || string.IsNullOrEmpty(presentPincode) || string.IsNullOrEmpty(presentAddressLine))
                {
                    throw new Exception("Please fill in all the required fields.");
                }

                if (Session["Phone"] != null && Session["Phone"].ToString() == phone)
                {
                    ErrorMessage.Text = "This User already exists";
                    return;
                }

                int userId = InsertUserDetails(firstName, middleName, lastName, gender, email, phone, dateOfBirth, fatherName, motherName);

                InsertAddress(userId, 1, permanentCountry, permanentState, permanentCity, permanentPincode, permanentAddressLine);

                InsertAddress(userId, 2, presentCountry, presentState, presentCity, presentPincode, presentAddressLine);

                Session["Phone"] = phone;
                flag = true;

                if (flag)
                {
                    ClearFormFields();

                    Email.SendEmail(email);

                    Response.Redirect("Users.aspx");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
            }
        }

        private int InsertUserDetails(string firstName, string middleName, string lastName, string gender, string email, string phone, string dateOfBirth, string fatherName, string motherName)
        {
            int userId;
            using (var con = Connection.Connect())
            {
                con.Open();

                string queryUser = "INSERT INTO UserDetails VALUES (@firstName, @middleName, @lastName, @gender, @email, @phone, @dateOfBirth, @fatherName, @motherName); ";

                using (SqlCommand cmd = new SqlCommand(queryUser, con))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@middleName", middleName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                    cmd.Parameters.AddWithValue("@fatherName", fatherName);
                    cmd.Parameters.AddWithValue("@motherName", motherName);

                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return userId;
        }

        private void InsertAddress(int userId, int addressType, string street, string city, string pincode, String country, String state)
        {
            using (var con = Connection.Connect())
            {
                con.Open();

                int countryId = GetCountryIdByName(country);
                int stateId = GetStateIdByNameAndCountry(state, countryId);

                string queryAddress = "INSERT INTO AddressDetails VALUES (@userId, @addressType, @street, @city, @pincode, @countryId, @stateId);";

                using (SqlCommand cmd = new SqlCommand(queryAddress, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@addressType", addressType);
                    cmd.Parameters.AddWithValue("@street", street);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@pincode", pincode);
                    cmd.Parameters.AddWithValue("@countryId", countryId);
                    cmd.Parameters.AddWithValue("@stateId", stateId);

                    cmd.ExecuteNonQuery();
                }
            }
        }




        private int GetStateIdByNameAndCountry(string stateName, int countryId)
        {
            using (var con = Connection.Connect())
            {
                con.Open();

                string query = "SELECT StateID FROM State WHERE StateName = @stateName AND CountryID = @countryId;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@stateName", stateName);
                    cmd.Parameters.AddWithValue("@countryId", countryId);

                    object result = cmd.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        private int GetCountryIdByName(string countryName)
        {
            using (var con = Connection.Connect())
            {
                con.Open();

                string query = "SELECT CountryID FROM Country WHERE CountryName = @countryName;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@countryName", countryName);

                    object result = cmd.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }



        private void ClearFormFields()
        {
            TxtFirstName.Text = "";
            TxtMiddleName.Text = "";
            TxtLastName.Text = "";
            TxtGender.Text = "";
            TxtEmailID.Text = "";
            TxtPhoneNumber.Text = "";
            TxtDateOfBirth.Text = "";
            TxtFatherName.Text = "";
            TxtMotherName.Text = "";
            PermanentCountry.SelectedIndex = -1;
            PermanentState.SelectedIndex = -1;
            PermanentCity.Text = "";
            PermanentPincode.Text = "";
            PermanentAddressLine.Text = "";
            PresentCountry.SelectedIndex = -1;
            PresentState.SelectedIndex = -1;
            PresentCity.Text = "";
            PresentPincode.Text = "";
            PresentAddressLine.Text = "";

            ErrorMessage.Text = "";
        }
    }
}
