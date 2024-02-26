using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.DataAccess;
using UserManagement.Utils;

namespace UserManagement.Business
{
    public class UserLogic
    {
        public static int LoginUser(string username, string password)
        {
            try
            {
                return UserDataAccess.LoginUser(username, password);
            }
            catch (Exception ex)
            {
                Logger.AddError("Login Failed", ex);
                return -1;
            }
        }

        public static bool IsAdmin(int userId)
        {
            return UserDataAccess.IsAdmin(userId);
        }
    }
}
