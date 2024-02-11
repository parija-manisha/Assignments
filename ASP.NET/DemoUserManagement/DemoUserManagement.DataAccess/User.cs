using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccess
{
    public class User
    {
        public class UserManager
        {
            private readonly UserRepository userRepository;
            private readonly AddressRepository addressRepository;

            public UserManager()
            {
                this.userRepository = new UserRepository();
                this.addressRepository = new AddressRepository();
            }

            public List<User> GetUsers()
            {
                return userRepository.GetUsers();
            }

            public int InsertUser(User user, Address permanentAddress, Address presentAddress)
            {
                int userId = userRepository.InsertUser(user);

                // Insert addresses
                addressRepository.InsertAddress(new Address(userId, 1, permanentAddress));
                addressRepository.InsertAddress(new Address(userId, 2, presentAddress));

                return userId;
            }

            // Add other business logic methods
        }
    }
}
