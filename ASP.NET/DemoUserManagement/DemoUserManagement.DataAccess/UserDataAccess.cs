using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccess
{
    internal class UserDataAccess
    {
        namespace DemoUserManagement.DataAccess
    {
        public class UserRepository
        {
            public List<User> GetUsers()
            {
                // Implementation to get users from the database
                throw new NotImplementedException();
            }

            public int InsertUser(User user)
            {
                // Implementation to insert user into the database
                throw new NotImplementedException();
            }

            // Add other methods for interacting with the database
        }

        public class AddressRepository
        {
            public void InsertAddress(Address address)
            {
                // Implementation to insert address into the database
                throw new NotImplementedException();
            }

            // Add other methods for interacting with the database
        }
    }

}
}
