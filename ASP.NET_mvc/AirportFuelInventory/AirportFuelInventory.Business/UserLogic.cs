using AirportFuelInventory.DataAccess;
using AirportFuelInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Business
{
    public class UserLogic
    {
        public static void NewUser(UserDTO userDTO)
        {
            UserDataAccess.NewUser(userDTO);
        }

        public static bool IsEmailExist(string email)
        {
            return UserDataAccess.IsEmailExist(email);
        }

        public static bool CheckLogin(string username, string password) { 
            return UserDataAccess.CheckLogin(username, password);
        }
    }
}
