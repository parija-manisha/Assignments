
using DemoUserManagement.DataAccess;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

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

        public static void SaveAddress(int userId, UserDetailDTO userDetailDTO)
        {
            try
            {
                UserDataAccess.SaveAddress(userId, userDetailDTO);
            }
            catch (Exception ex)
            {
                Logger.AddError("SaveUser Failed", ex);
                throw;
            }
        }

        public static void SaveRole(int userID)
        {
            UserDataAccess.SaveRole(userID);
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

        public static List<UserDetailDTO> GetAllUsers(int start, int length, string sortColumn, string sortDirection)
        {
            try
            {
                return UserDataAccess.GetAllUsers(start, length, sortColumn, sortDirection);
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

        public static int GetTotalRecords()
        {
            return UserDataAccess.GetTotalRecords();
        }

        public static int GetFilteredRecords(string sortColumn, string sortDirection)
        {
            return UserDataAccess.GetFilteredRecords(sortColumn, sortDirection);
        }

        public static int GetUserID(string userName, string password)
        {
            return UserDataAccess.GetUserID(userName, password);
        }


        public static bool IsAdmin(int userId)
        {
            return UserDataAccess.IsAdmin(userId);
        }

        public static bool IsEmailExists(string email)
        {
            return UserDataAccess.IsEmailExists(email);
        }

        public static List<RoleDTO> GetRoleList()
        {
            List<Role> roles = UserDataAccess.GetRole();
            List<RoleDTO> roleList = roles.Select(role => new RoleDTO
            {
                RoleID = role.RoleID,
                RoleName = role.RoleName
            }).ToList();

            return roleList;
        }

        public static object UploadFile(HttpPostedFileBase file)
        {
            return DocumentDataAccess.UploadFile(file);
        }

    }
}