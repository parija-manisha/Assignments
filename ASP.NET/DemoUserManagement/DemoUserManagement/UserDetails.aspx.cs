using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;

namespace DemoUserManagement
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Phone"] != null)
                {
                    ErrorMessage.Text = "This User already exists";
                }

                PopulateCountries();
                ShowButton(false);

                ErrorMessage.Text = "";
            }
        }

        private void ShowButton(bool button)
        {
            if ((button))
            {
                SaveUserButton.Visible = false;
                UpdateUserButton.Visible = true;
                DeleteUserButton.Visible = true;
            }

            else
            {
                SaveUserButton.Visible = true;
                UpdateUserButton.Visible = false;
                DeleteUserButton.Visible = false;
            }
        }

        private void BindDropDownList<T>(DropDownList ddl, List<T> list, string textField, string valueField)
        {
            ddl.DataSource = list;
            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", ""));
        }

        private void PopulateCountries()
        {
            List<CountryDTO> countryList = CountryLogic.GetCountryList();

            BindDropDownList(DdlPresentCountry, countryList, "CountryName", "CountryId");
            BindDropDownList(DdlPermanentCountry, countryList, "CountryName", "CountryId");
        }

        private void PopulateStates(DropDownList ddl, int countryId)
        {
            List<StateDTO> stateList = StateLogic.GetStateList(countryId);
            BindDropDownList(ddl, stateList, "StateName", "StateId");
        }

        protected void PresentCountryState(object sender, EventArgs e)
        {
            int selectedCountryId = Convert.ToInt32(DdlPresentCountry.SelectedValue);
            PopulateStates(DdlPresentState, selectedCountryId);
        }

        protected void PermanentCountryState(object sender, EventArgs e)
        {
            int selectedCountryId = Convert.ToInt32(DdlPermanentCountry.SelectedValue);
            PopulateStates(DdlPermanentState, selectedCountryId);
        }

        protected void SameAsPermanent_CheckedChanged(object sender, EventArgs e)
        {
            if (SameAsPermanent.Checked)
            {
                DdlPresentCountry.SelectedValue = DdlPermanentCountry.SelectedValue;
                DdlPresentState.SelectedValue = DdlPermanentState.SelectedValue;
                DdlPresentCity.Text = DdlPermanentCity.Text;
                DdlPresentPincode.Text = DdlPermanentPincode.Text;
                DdlPresentAddressLine.Text = DdlPermanentPincode.Text;
            }
            else
            {
                DdlPresentCountry.SelectedIndex = 0;
                DdlPresentState.SelectedIndex = 0;
                DdlPresentCity.Text = "";
                DdlPresentPincode.Text = "";
                DdlPresentAddressLine.Text = "";
            }
        }

        protected void SaveUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                UserDetailDTO user = CreateUserFromForm();
                if (ArePropertiesNullOrEmpty(user))
                {
                    ErrorMessage.Text = "Please fill in all the required fields.";
                    return; 
                }
                UserLogic.SaveUser(user);
                Session["Phone"] = TxtPhoneNumber.Text;

                ResetFormFields();

                Response.Redirect("Users.aspx");
            }
            catch (Exception ex)
            {
                Logger.AddError("Registration Failed", ex);
                ErrorMessage.Text = "An error occurred during registration";
            }
        }

        private UserDetailDTO CreateUserFromForm()
        {
            UserDetailDTO user = new UserDetailDTO
            {
                FirstName = TxtFirstName.Text,
                MiddleName = TxtMiddleName.Text,
                LastName = TxtLastName.Text,
                Gender = TxtGender.Text,
                Email = TxtEmailID.Text,
                PhoneNumber = int.TryParse(TxtPhoneNumber.Text, out int phone) ? (int?)phone : null,
                DateOfBirth = DateTime.TryParse(TxtDateOfBirth.Text, out DateTime dateOfBirth) ? (DateTime?)dateOfBirth : null,
                FatherName = TxtFatherName.Text,
                MotherName = TxtMotherName.Text
            };
            ArePropertiesNullOrEmpty(user);
            return user;
        }

        public bool ArePropertiesNullOrEmpty(object obj)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);

                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    return true; 
                }
            }

            return false;
        }

        private void ResetFormFields()
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
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(Request.QueryString["UserId"]);
            UserDetailDTO user = new UserDetailDTO
            {
                FirstName = TxtFirstName.Text,
                MiddleName = TxtMiddleName.Text,
                LastName = TxtLastName.Text,
                Gender = TxtGender.Text,
                Email = TxtEmailID.Text,
                PhoneNumber = int.TryParse(TxtPhoneNumber.Text, out int phone) ? (int?)phone : null,
                DateOfBirth = DateTime.TryParse(TxtDateOfBirth.Text, out DateTime dateOfBirth) ? (DateTime?)dateOfBirth : null,
                FatherName = TxtFatherName.Text,
                MotherName = TxtMotherName.Text,

            };
            UserLogic.UpdateUser(userId, user);
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(Request.QueryString["ID"]);
            UserLogic.DeleteUser(userId);
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Deleted User Successfully');", true);
            Response.Redirect("UserList.aspx");
        }
    }
}