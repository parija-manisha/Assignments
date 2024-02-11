using DemoUserManagement.DataAccess;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DemoUserManagement.Business
{
    public class UserManager
    {
        public bool InsertUser(string firstName, string middleName, string lastName, string gender, string email,
                 string phone, string dateOfBirth, string fatherName, string motherName)
        {
            try
            {
                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                {
                    throw new Exception("Please fill in all the required fields");
                }

                UserDataAccess.InsertUser(firstName, middleName, lastName, gender, email, phone, dateOfBirth, fatherName, motherName);
                HttpContext.Current.Session["phone"] = phone;

                EmailSend.SendEmail(email);

                return true;
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
                return false;
            }
        }
    }
}
