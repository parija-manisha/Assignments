

using DemoUserManagement.Models;
using System.Linq;

namespace DemoUserManagement.DataAccess
{
    public class AddressDataAccess
    {
        public static void SaveAddress(AddressDetailDTO addressDetailDTO)
        {
            using (UserManagementTableEntities context = new UserManagementTableEntities())
            {
                if (addressDetailDTO.UserID > 0)
                {
                    UpdateAddress(addressDetailDTO.UserID, addressDetailDTO);
                }
                else
                {
                    AddressDetail user = new AddressDetail
                    {
                        UserID = addressDetailDTO.UserID,
                        AddressType = addressDetailDTO.AddressType,
                        Street = addressDetailDTO.Street,
                        City = addressDetailDTO.City,
                        Pincode = addressDetailDTO.Pincode,
                        CountryID = addressDetailDTO.CountryID,
                        StateID = (int)addressDetailDTO.StateID
                    };
                    context.AddressDetails.Add(user);
                    context.SaveChanges();
                }
            }
        }

        public static void UpdateAddress(int userId, AddressDetailDTO addressDetailDTO)
        {
            using (UserManagementTableEntities context = new UserManagementTableEntities())
            {
                if (addressDetailDTO.AddressType == 2) 
                {
                    var presentAddress = context.AddressDetails.FirstOrDefault(a => a.UserID == userId && a.AddressType == 2);
                    if (presentAddress != null)
                    {
                        presentAddress.Street = addressDetailDTO.Street;
                        presentAddress.City = addressDetailDTO.City;
                        presentAddress.Pincode = addressDetailDTO.Pincode;
                        presentAddress.CountryID = addressDetailDTO.CountryID;
                        presentAddress.StateID = (int)addressDetailDTO.StateID;
                    }
                }
                else if (addressDetailDTO.AddressType == 1) 
                {
                    var permanentAddress = context.AddressDetails.FirstOrDefault(a => a.UserID == userId && a.AddressType == 1);
                    if (permanentAddress != null)
                    {
                        permanentAddress.Street = addressDetailDTO.Street;
                        permanentAddress.City = addressDetailDTO.City;
                        permanentAddress.Pincode = addressDetailDTO.Pincode;
                        permanentAddress.CountryID = addressDetailDTO.CountryID;
                        permanentAddress.StateID = (int)addressDetailDTO.StateID;
                    }
                }

                context.SaveChanges();
            }
        }
    }
}

