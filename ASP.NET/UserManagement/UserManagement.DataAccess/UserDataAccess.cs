using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Utils;

namespace UserManagement.DataAccess
{
    public class UserDataAccess
    {
        public static int LoginUser(string username, string password)
        {
            try
            {
                using (var context = new UserManagementTableEntities())
                {
                    int userId = context.UserDetails.Where(u => u.Email == username && u.Password.Equals(password, StringComparison.Ordinal))
                        .Select(u => u.UserID)
                        .FirstOrDefault();

                    if (userId != 0)
                    {
                        return userId;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("UserId authentication Failed", ex);
                return -1;
            }
        }

        public static bool IsAdmin(int userId)
        {
            try
            {
                using( var context =  new UserManagementTableEntities())
                {
                    bool result = context.UserRoles.Any(ur => ur.UserID == userId && ur.Role.isAdmin == "true");

                    return result;
                }
            }
            catch(Exception ex)
            {
                Logger.AddError("Failed\n", ex);
                return false;
            }
        }

    }
}
