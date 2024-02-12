

using DemoUserManagement.Models;

namespace DemoUserManagement.DataAccess
{
    public class AddressDataAccess
    {
        public static void SaveAddress(AddressDetailDTO address)
        {
            using (UserManagementTableEntities2 context = new UserManagementTableEntities2())
            {
                AddressDetail user = new AddressDetail
                {
                    UserID = address.UserID,
                    AddressType = address.AddressType,
                    Street = address.Street,
                    City = address.City,
                    Pincode = address.Pincode,
                    CountryID = address.CountryID,
                    StateID=address.StateID
                };
                context.AddressDetails.Add(user);
                context.SaveChanges();
            }
        }
    }
}
