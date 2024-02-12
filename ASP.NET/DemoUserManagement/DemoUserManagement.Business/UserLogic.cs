using DemoUserManagement.DataAccess;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Business
{
    public class UserLogic
    {
        public static void SaveUser(UserDetailDTO userDetailDTO)
        {
            try
            {
                UserDataAccess.SaveUser(userDetailDTO);
            }
            catch (Exception ex)
            {
                Logger.AddError("SaveUser Failed", ex);
                throw; 
            }
        }

        public static void UpdateUser(int userId, UserDetailDTO userDetailDTO)
        {
            try
            {
                UserDataAccess.UpdateUser(userId, userDetailDTO);
            }
            catch (Exception ex)
            {
                Logger.AddError("UpdateUser Failed", ex);
                throw; 
            }
        }

        public static void DeleteUser(int userId)
        {
            try
            {
                UserDataAccess.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                Logger.AddError("DeleteUser Failed", ex);
                throw; 
            }
        }

        public List<UserDetailDTO> GetAllUsers()
        {
            GetUser user = new GetUser();
            return user.Users();
        }
    }
}
