using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
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
        public static int SaveUser(Dictionary<string, string> userDetails, List<Dictionary<string, string>> addressDetails)
        {
            try
            {
                UserDetailDTO user = CreateUserFromDictionary(userDetails);

                List<AddressDetailDTO> addresses = new List<AddressDetailDTO>();
                foreach (var addressDetail in addressDetails)
                {
                    AddressDetailDTO address = CreateAddressFromDictionary(addressDetail, user.UserID, addressDetail["AddressType"]);
                    addresses.Add(address);
                }

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

        private static UserDetailDTO CreateUserFromDictionary(Dictionary<string, string> userDetails)
        {
            UserDetailDTO user = new UserDetailDTO
            {
                UserID = Convert.ToInt32(userDetails["UserID"]),
                FirstName = userDetails["FirstName"],
                MiddleName = userDetails["MiddleName"],
                LastName = userDetails["LastName"],
                Gender = userDetails["Gender"],
                Email = userDetails["Email"],
                PhoneNumber = Convert.ToInt32(userDetails["PhoneNumber"]),
                DateOfBirth = Convert.ToDateTime(userDetails["DateOfBirth"]),
                Hobbies = userDetails["Hobbies"],
                FatherName = userDetails["FatherName"],
                MotherName = userDetails["MotherName"],
                Password = userDetails["Password"],
                ConfirmPassword = userDetails["ConfirmPassword"],
            };

            return user;
        }

        private static AddressDetailDTO CreateAddressFromDictionary(Dictionary<string, string> userDetails, int userId, string addressType)
        {
            AddressDetailDTO address = new AddressDetailDTO();

            if (addressType.Equals("Present", StringComparison.OrdinalIgnoreCase))
            {
                address.AddressType = 2;
            }
            else if (addressType.Equals("Permanent", StringComparison.OrdinalIgnoreCase))
            {
                address.AddressType = 1;
            }

            address.Street = userDetails[$"{addressType}Street"];
            address.City = userDetails[$"{addressType}City"];
            address.Pincode = Convert.ToInt32(userDetails[$"{addressType}Pincode"]);
            address.CountryID = Convert.ToInt32(userDetails[$"{addressType}Country"]);
            address.StateID = Convert.ToInt32(userDetails[$"{addressType}State"]);
            address.UserID = userId;

            return address;
        }

        [WebMethod]
        public static List<StateDTO> PopulateState(int countryId)
        {
            try
            {
                List<StateDTO> stateList = StateLogic.GetStateList(countryId);
                return stateList;
            }
            catch (Exception ex)
            {
                Logger.AddError("Error in PopulateState method", ex);
                throw;
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

    }
}
