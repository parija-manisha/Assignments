using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class UserDetails_v2 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static int SaveUser(UserDetailDTO userDetails, List<AddressDetailDTO> addressDetails)
        {
            try
            {
                UserDetailDTO user = CreateUser(userDetails);

                List<AddressDetailDTO> addresses = CreateAddress(addressDetails);

                int userId = UserLogic.SaveUser(user);

                foreach (var address in addresses)
                {
                    address.UserID = userId;
                    UserLogic.SaveAddress(address);
                }

                if (userId != -1)
                {
                    UserLogic.SaveRole(userId);
                    if (UserLogic.IsAdmin(userId))
                    {
                        HttpContext.Current.Session["UserID"] = userId;
                        return -2;
                    }
                    else
                    {
                        HttpContext.Current.Session["UserID"] = userId;
                        return userId;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Registration Failed", ex);
                return -1;
            }
        }

        private static UserDetailDTO CreateUser(UserDetailDTO userDetails)
        {
            UserDetailDTO user = new UserDetailDTO
            {
                UserID = Convert.ToInt32(userDetails.UserID),
                FirstName = userDetails.FirstName,
                MiddleName = userDetails.MiddleName,
                LastName = userDetails.LastName,
                Gender = userDetails.Gender,
                Email = userDetails.Email,
                PhoneNumber = Convert.ToInt32(userDetails.PhoneNumber),
                DateOfBirth = Convert.ToDateTime(userDetails.DateOfBirth),
                Hobbies = userDetails.Hobbies,
                FatherName = userDetails.FatherName,
                MotherName = userDetails.MotherName,
                Password = userDetails.Password,
                ConfirmPassword = userDetails.ConfirmPassword
            };

            return user;
        }

        private static List<AddressDetailDTO> CreateAddress(List<AddressDetailDTO> addressDetails)
        {
            List<AddressDetailDTO> addresses = new List<AddressDetailDTO>();

            foreach (var detail in addressDetails)
            {
                AddressDetailDTO presentAddress = new AddressDetailDTO
                {
                    AddressType = 2,
                    Street = detail.Street,
                    City = detail.City,
                    Pincode = Convert.ToInt32(detail.Pincode),
                    CountryID = Convert.ToInt32(detail.CountryID),
                    StateID = Convert.ToInt32(detail.StateID),
                };
                addresses.Add(presentAddress);

                AddressDetailDTO permanentAddress = new AddressDetailDTO
                {
                    AddressType = 1,
                    Street = detail.Street,
                    City = detail.City,
                    Pincode = Convert.ToInt32(detail.Pincode),
                    CountryID = Convert.ToInt32(detail.CountryID),
                    StateID = Convert.ToInt32(detail.StateID),
                };
                addresses.Add(permanentAddress);
            }

            return addresses;
        }


        [WebMethod]
        public static object PopulateState(int countryId)
        {
            try
            {
                List<StateDTO> stateList = StateLogic.GetStateList(countryId);
                return new { success = true, data = stateList };
            }
            catch (Exception ex)
            {
                Logger.AddError("Error in PopulateState method", ex);
                return new { success = false, message = "An error occurred while populating states." };
            }
        }

        [WebMethod]
        public static List<CountryDTO> GetCountries()
        {
            List<CountryDTO> countries = CountryLogic.GetCountryList();
            return countries;
        }

        [WebMethod]
        public static void CopyPermanentAddress(bool sameAsPermanent, string permanentCountry, string permanentState, string permanentCity, string permanentPincode, string permanentAddressLine)
        {
            HttpContext.Current.Session["SameAsPermanent"] = sameAsPermanent;

            if (sameAsPermanent)
            {
                HttpContext.Current.Session["PresentCountry"] = permanentCountry;
                HttpContext.Current.Session["PresentState"] = permanentState;
                HttpContext.Current.Session["PresentCity"] = permanentCity;
                HttpContext.Current.Session["PresentPincode"] = permanentPincode;
                HttpContext.Current.Session["PresentAddressLine"] = permanentAddressLine;
            }
            else
            {
                HttpContext.Current.Session["PresentCountry"] = null;
                HttpContext.Current.Session["PresentState"] = null;
                HttpContext.Current.Session["PresentCity"] = null;
                HttpContext.Current.Session["PresentPincode"] = null;
                HttpContext.Current.Session["PresentAddressLine"] = null;
            }
        }

        [WebMethod]
        public static UserDetailDTO GetUserDetails(int userId)
        {
            try
            {
                return UserLogic.GetUserById(userId);
            }
            catch (Exception ex)
            {
                Logger.AddError("Error in GetUserDetails method", ex);
                return null; 
            }
        }


    }
}
