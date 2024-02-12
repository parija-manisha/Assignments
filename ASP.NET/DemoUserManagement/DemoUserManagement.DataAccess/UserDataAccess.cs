using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccess
{
    public class UserDataAccess
    {
        public static void SaveUser(UserDetailDTO userDetailDTO)
        {
            using (var context = new UserManagementTableEntities2())
            {
                UserDetail user = new UserDetail
                {
                    FirstName = userDetailDTO.FirstName,
                    MiddleName = userDetailDTO.MiddleName,
                    LastName = userDetailDTO.LastName,
                    Gender = userDetailDTO.Gender,
                    Email = userDetailDTO.Email,
                    PhoneNumber = userDetailDTO.PhoneNumber,
                    DateOfBirth = userDetailDTO.DateOfBirth,
                    FatherName = userDetailDTO.FatherName,
                    MotherName = userDetailDTO.MotherName,
                    
                };
                context.UserDetails.Add(user);
                context.SaveChanges();
            }
        }

        public static void UpdateUser(int userId, UserDetailDTO userDetailDTO)
        {
            using (UserManagementTableEntities2 context = new UserManagementTableEntities2())
            {
                UserDetail user = context.UserDetails.FirstOrDefault(x => x.UserID == userId);
                if (user != null)
                {
                    user.FirstName = userDetailDTO.FirstName;
                    user.MiddleName = userDetailDTO.MiddleName;
                    user.LastName = userDetailDTO.LastName;
                    user.Gender = userDetailDTO.Gender;
                    user.Email = userDetailDTO.Email;
                    user.PhoneNumber = userDetailDTO.PhoneNumber;
                    user.DateOfBirth = userDetailDTO.DateOfBirth;
                    user.FatherName = userDetailDTO.FatherName;
                    user.MotherName = userDetailDTO.MotherName;

                    context.SaveChanges();
                }
            }
        }

        public static void DeleteUser(int userId)
        {
            using (UserManagementTableEntities2 context = new UserManagementTableEntities2())
            {
                UserDetail user = context.UserDetails.FirstOrDefault(x => x.UserID == userId);
                if (user != null)
                {
                    context.UserDetails.Remove(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
