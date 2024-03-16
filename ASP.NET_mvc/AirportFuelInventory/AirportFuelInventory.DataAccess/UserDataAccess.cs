using AirportFuelInventory.Utils;
using System;
using System.Linq;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.DataAccess
{
    public class UserDataAccess
    {
        public static bool NewUser(UserDTO userDTO)
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
                return true;    
            }
        }

        public static bool IsEmailExist(string email)
        {
            using (var context = new AirportFuelInventoryEntities())
            {
                return context.Users.Any(u => u.Email == email);
            }
        }

        public static int CheckLogin(string username, string password)
        {
            int userID = -1;
            using (var context = new AirportFuelInventoryEntities())
            {
                try
                {
                    var user = context.Users
                        .Where(u => u.Email == username && u.Password == password)
                        .Select(u => new { u.User_Id })
                        .FirstOrDefault();

                    if (user != null)
                    {
                        userID = user.User_Id;
                    }
                }
                catch (Exception ex)
                {
                    Logger.AddError("Error in GetUserID method", ex);
                }
            }

            return userID;
        }
    }
}