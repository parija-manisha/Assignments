
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
        public static int SaveUser(UserDetailDTO userDetailDTO)
        {
            try
            {
                int userId = UserDataAccess.SaveUser(userDetailDTO);
                return userId;
            }
            catch (Exception ex)
            {
                Logger.AddError("SaveUser Failed", ex);
                throw;
            }
        }

        public static UserDetailDTO GetUserById(int userId)
        {
            try
            {
                return UserDataAccess.GetUserById(userId);
            }
            catch (Exception ex)
            {
                Logger.AddError("GetUserById Failed", ex);
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

        public static void SaveAddress(AddressDetailDTO addressDetailDTO)
        {
            try
            {
                AddressDataAccess.SaveAddress(addressDetailDTO);
            }
            catch (Exception ex)
            {
                Logger.AddError("SaveUser Failed", ex);
                throw;
            }
        }

        public static List<UserDetailDTO> GetAllUsers()
        {
            try
            {
                GetUser user = new GetUser();
                return user.Users();
            }
            catch (Exception ex)
            {
                Logger.AddError("GetAllUsers Failed", ex);
                throw;
            }
        }

        public static string GetFileNameFromFileGuid(string fileNameGuid)
        {
            try
            {
                string fileName = UserDataAccess.GetFileNameByFileGuid(fileNameGuid);
                if (fileName != null)
                {
                    return fileName;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Failed", ex);
                return null;
            }
        }

        //public static List<UserDetailDTO> GetUsers(int pageSize, int pageIndex, string sortBy, string sortDirection)
        //{
        //    try
        //    {
        //        return UserDataAccess.GetUsers(pageSize, pageIndex, sortBy, sortDirection);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.AddError("Error in business logic while fetching users", ex);
        //        throw;
        //    }
        //}
    }
}