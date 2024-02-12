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
                ErrorMessage.Text = "";
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

        private List<AddressDetailDTO> CreateAddressesFromForm()
        {
            List<AddressDetailDTO> addresses = new List<AddressDetailDTO>();

            AddressDetailDTO presentAddress = new AddressDetailDTO
            {
                AddressType = 1,
                Street = DdlPresentAddressLine.Text,
                City = DdlPresentCity.Text,
                Pincode = int.TryParse(DdlPresentPincode.Text, out int presentPincode) ? (int?)presentPincode : null,
                CountryID = GetCountryIdByName(DdlPresentCountry.SelectedValue),
                StateID = GetStateIdByName(DdlPresentState.SelectedValue, GetCountryIdByName(DdlPresentCountry.SelectedValue))
            };
            addresses.Add(presentAddress);

            AddressDetailDTO permanentAddress = new AddressDetailDTO
            {
                AddressType = 2,
                Street = DdlPermanentAddressLine.Text,
                City = DdlPermanentCity.Text,
                Pincode = int.TryParse(DdlPermanentPincode.Text, out int permanentPincode) ? (int?)permanentPincode : null,
                CountryID = GetCountryIdByName(DdlPermanentCountry.SelectedValue),
                StateID = GetStateIdByName(DdlPermanentState.SelectedValue, GetCountryIdByName(DdlPermanentCountry.SelectedValue))
            };
            addresses.Add(permanentAddress);

            return addresses;
        }

        private int GetCountryIdByName(string countryName)
        {
            List<CountryDTO> countryList = CountryLogic.GetCountryList();

            CountryDTO country = countryList.Find(c => c.CountryName == countryName);

            return country?.CountryID ?? 0;
        }

        private int GetStateIdByName(string stateName, int countryID)
        {
            List<StateDTO> stateList = StateLogic.GetStateList(countryID);

            StateDTO state = stateList.Find(c => c.StateName == stateName);

            return state?.StateID ?? 0;
        }

        public bool ArePropertiesNullOrEmpty(object obj)
        {
            if (obj == null)
            {
                return true;
            }

            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);

                if (value == null || (value is string && string.IsNullOrEmpty((string)value)))
                {
                    return true;
                }
            }

            return false;
        }

        protected void SaveUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                UserDetailDTO user = CreateUserFromForm();
                List<AddressDetailDTO> addresses = CreateAddressesFromForm();
                if (ArePropertiesNullOrEmpty(user) || ArePropertiesNullOrEmpty(addresses))
                {
                    ErrorMessage.Text = "Please fill in all the required fields.";
                    return;
                }
                UserLogic.SaveUser(user);
                foreach (var address in addresses)
                {
                    UserLogic.SaveAddress(address);
                }
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