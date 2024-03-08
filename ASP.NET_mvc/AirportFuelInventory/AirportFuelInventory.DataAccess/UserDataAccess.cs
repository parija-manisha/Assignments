using AirportFuelInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.DataAccess
{
    public class UserDataAccess
    {
        public static void NewUser(UserDTO userDTO)
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                User user = new User
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static bool IsEmailExist(string email)
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                return context.Users.Any(u => u.Email == email);
            }
        }

        public static bool CheckLogin(string username, string password)
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                return context.Users.Any(u => u.Email == username && String.Equals(u.Password, password, StringComparison.Ordinal));
            }
        }

    }
}