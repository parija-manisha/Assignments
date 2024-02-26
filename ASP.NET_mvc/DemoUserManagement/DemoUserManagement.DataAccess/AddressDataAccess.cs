using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System.Linq;

namespace DemoUserManagement.DataAccess
{
    public class AddressDataAccess
    {
        public static void SaveAddress(AddressDetailDTO addressDetailDTO)
        {
            using (UserManagementTableEntities context = new UserManagementTableEntities())
            {
                int userId = addressDetailDTO.UserID;
                if (addressDetailDTO.AddressType == Constants.AddressType.PresentAddress)
                {
                    var presentAddress = context.AddressDetails.FirstOrDefault(a => a.UserID == userId && a.AddressType == Constants.AddressType.PresentAddress);
                    if (presentAddress != null)
                    {
                        presentAddress.Street = addressDetailDTO.Street;
                        presentAddress.City = addressDetailDTO.City;
                        presentAddress.Pincode = addressDetailDTO.Pincode;
                        presentAddress.CountryID = addressDetailDTO.CountryID;
                        presentAddress.StateID = (int)addressDetailDTO.StateID;
                    }
                    else
                    {
                        AddressDetail newPresentAddress = new AddressDetail
                        {
                            UserID = userId,
                            AddressType = Constants.AddressType.PresentAddress,
                            Street = addressDetailDTO.Street,
                            City = addressDetailDTO.City,
                            Pincode = addressDetailDTO.Pincode,
                            CountryID = addressDetailDTO.CountryID,
                            StateID = (int)addressDetailDTO.StateID
                        };
                        context.AddressDetails.Add(newPresentAddress);
                    }
                }
                else if (addressDetailDTO.AddressType == Constants.AddressType.PermanentAddress)
                {
                    var permanentAddress = context.AddressDetails.FirstOrDefault(a => a.UserID == userId && a.AddressType == Constants.AddressType.PermanentAddress);
                    if (permanentAddress != null)
                    {
                        permanentAddress.Street = addressDetailDTO.Street;
                        permanentAddress.City = addressDetailDTO.City;
                        permanentAddress.Pincode = addressDetailDTO.Pincode;
                        permanentAddress.CountryID = addressDetailDTO.CountryID;
                        permanentAddress.StateID = (int)addressDetailDTO.StateID;
                    }
                    else
                    {
                        AddressDetail newPermanentAddress = new AddressDetail
                        {
                            UserID = userId,
                            AddressType = Constants.AddressType.PermanentAddress,
                            Street = addressDetailDTO.Street,
                            City = addressDetailDTO.City,
                            Pincode = addressDetailDTO.Pincode,
                            CountryID = addressDetailDTO.CountryID,
                            StateID = (int)addressDetailDTO.StateID
                        };
                        context.AddressDetails.Add(newPermanentAddress);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
