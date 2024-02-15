using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            ucNoteControl.ObjectType = Constants.ObjectType.UserDetail;
            ucNoteControl.ObjectIDName = Constants.ObjectIDName.UserID;
            ucDocumentUserControl.ObjectType = Constants.ObjectType.UserDetail;
            ucDocumentUserControl.ObjectIDName = Constants.ObjectIDName.UserID;

            if (!IsPostBack)
            {

                if (Session["Phone"] != null)
                {
                    ErrorMessage.Text = "This User already exists";
                }

                PopulateCountries();
                DdlPresentCountry.SelectedIndexChanged += PresentCountryState;
                DdlPermanentCountry.SelectedIndexChanged += PermanentCountryState;


                if (!string.IsNullOrEmpty(Request.QueryString[ucNoteControl.ObjectIDName]))
                {
                    int userId = Convert.ToInt32(Request.QueryString[ucNoteControl.ObjectIDName]);
                    LoadUserDetails(userId);
                }


                if (!string.IsNullOrEmpty(Request.QueryString[ucNoteControl.ObjectIDName]))
                {
                    ucNoteControl.Visible = true;
                }
                else
                {
                    ucNoteControl.Visible = false;
                }
            }
        }

        private void LoadUserDetails(int userId)
        {
            UserDetailDTO user = UserLogic.GetUserById(userId);

            if (user !=
                null)
            {
                TxtFirstName.Text = user.FirstName;
                TxtMiddleName.Text = user.MiddleName;
                TxtLastName.Text = user.LastName;
                TxtGender.Text = user.Gender;
                TxtEmailID.Text = user.Email;
                TxtPhoneNumber.Text = user.PhoneNumber.ToString();
                TxtDateOfBirth.Text = user.DateOfBirth.ToString();
                TxtFatherName.Text = user.FatherName;
                TxtMotherName.Text = user.MotherName;

                if (user.PresentAddress != null)
                {
                    DdlPresentAddressLine.Text = user.PresentAddress.Street;
                    DdlPresentCity.Text = user.PresentAddress.City;
                    DdlPresentPincode.Text = user.PresentAddress.Pincode.ToString();
                    DdlPresentCountry.SelectedValue = user.PresentAddress.CountryID.ToString();
                    PopulateStates(DdlPresentState, user.PresentAddress.CountryID);
                    DdlPresentState.SelectedValue = user.PresentAddress.StateID.ToString();
                }

                if (user.PermanentAddress != null)
                {
                    DdlPermanentAddressLine.Text = user.PermanentAddress.Street;
                    DdlPermanentCity.Text = user.PermanentAddress.City;
                    DdlPermanentPincode.Text = user.PermanentAddress.Pincode.ToString();
                    DdlPermanentCountry.SelectedValue = user.PermanentAddress.CountryID.ToString();
                    PopulateStates(DdlPermanentState, user.PermanentAddress.CountryID);
                    DdlPermanentState.SelectedValue = user.PermanentAddress.StateID.ToString();
                }
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
            if (countryId > 0)
            {
                List<StateDTO> stateList = StateLogic.GetStateList(countryId);
                BindDropDownList(ddl, stateList, "StateName", "StateId");
            }
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
                DdlPresentAddressLine.Text = DdlPermanentAddressLine.Text;
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
                ucDocumentUserControl.LoadDocumentDetails();
                UserDetailDTO user = CreateUserFromForm();
                List<AddressDetailDTO> addresses = CreateAddressesFromForm();

                int userId = UserLogic.SaveUser(user);

                foreach (var address in addresses)
                {
                    address.UserID = userId;
                    UserLogic.SaveAddress(address);
                }
                Session["Phone"] = TxtPhoneNumber.Text;

                if (userId != -1)
                {
                    ucDocumentUserControl.UploadFile(userId);
                    ResetFormFields();
                    Response.Redirect("Users.aspx", false);
                }
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
                PhoneNumber = int.Parse(TxtPhoneNumber.Text),
                DateOfBirth = DateTime.Parse(TxtDateOfBirth.Text),
                FatherName = TxtFatherName.Text,
                MotherName = TxtMotherName.Text,
                FileNameGuid = ucDocumentUserControl.FileNameGuid,
                FileName = ucDocumentUserControl.FileName,

                UserID = (!string.IsNullOrEmpty(Request.QueryString[Constants.ObjectIDName.UserID])) ? int.Parse(Request.QueryString[Constants.ObjectIDName.UserID]) : 0

            };

            ArePropertiesNullOrEmpty(user);
            return user;
        }

        private List<AddressDetailDTO> CreateAddressesFromForm()
        {
            List<AddressDetailDTO> addresses = new List<AddressDetailDTO>();

            AddressDetailDTO presentAddress = new AddressDetailDTO
            {
                AddressType = 2,
                Street = DdlPresentAddressLine.Text,
                City = DdlPresentCity.Text,
                Pincode = int.Parse(DdlPresentPincode.Text),
                CountryID = int.Parse(DdlPresentCountry.SelectedValue),
                StateID = int.Parse(DdlPresentState.SelectedValue),

                UserID = (!string.IsNullOrEmpty(Request.QueryString[Constants.ObjectIDName.UserID])) ? int.Parse(Request.QueryString[Constants.ObjectIDName.UserID]) : 0
            };
            addresses.Add(presentAddress);

            AddressDetailDTO permanentAddress = new AddressDetailDTO
            {
                AddressType = 1,
                Street = DdlPermanentAddressLine.Text,
                City = DdlPermanentCity.Text,
                Pincode = int.Parse(DdlPermanentPincode.Text),
                CountryID = int.Parse(DdlPermanentCountry.SelectedValue),
                StateID = int.Parse(DdlPermanentState.SelectedValue),

                UserID = (!string.IsNullOrEmpty(Request.QueryString[Constants.ObjectIDName.UserID])) ? int.Parse(Request.QueryString[Constants.ObjectIDName.UserID]) : 0
            };
            addresses.Add(permanentAddress);

            return addresses;
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(Request.QueryString["ID"]);
            UserLogic.DeleteUser(userId);
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Deleted User Successfully');", true);
            Response.Redirect("UserList.aspx");
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
    }
}