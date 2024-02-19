using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Services;
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
                PopulateCountries();
                PopulateRoles();
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
                    DeleteUserButton.Visible = true;
                    AddRoleToUser.Visible = true;
                }
                else
                {
                    ucNoteControl.Visible = false;
                    DeleteUserButton.Visible = false;
                    AddRoleToUser.Visible = false;
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
                //ucDocumentUserControl.LoadDocumentDetails();
                UserDetailDTO user = CreateUserFromForm();
                List<AddressDetailDTO> addresses = CreateAddressesFromForm();

                int userId = UserLogic.SaveUser(user);
                Session["UserID"] = userId;

                foreach (var address in addresses)
                {
                    address.UserID = userId;
                    UserLogic.SaveAddress(address);
                }

                if (userId != -1)
                {
                    //ucDocumentUserControl.UploadFile(userId);
                    UserLogic.SaveRole(userId);
                    if (UserLogic.IsAdmin(userId))
                    {
                        Response.Redirect("Users.aspx", false);
                    }
                    else
                    {
                        ErrorMessage.Text = "User Added successfully";
                    }
                    ResetFormFields();
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
                Password = TxtPassword.Text,
                ConfirmPassword = TxtConfirmPassword.Text,

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
            foreach (Control control in Page.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
                else if (control is DropDownList)
                {
                    ((DropDownList)control).ClearSelection();
                }
            }
        }

        protected void TxtEmailID_TextChanged(object sender, EventArgs e)
        {
            string email = TxtEmailID.Text;
            bool emailExists = UserLogic.IsEmailExists(email);

            if (emailExists)
            {
                LblEmailExists.Text = "Email already exists!";
                SaveUserButton.Enabled = false;
            }
            else
            {
                LblEmailExists.Text = "";
                SaveUserButton.Enabled = true;
            }
        }

        private void PopulateRoles()
        {
            List<RoleDTO> roleList = UserLogic.GetRoleList();
            BindDropDownList(DdlAddRole, roleList, "RoleName", "RoleID");
        }

        protected void AddRole(object sender, EventArgs e)
        {
            PopulateRoles();
        }
    }
}