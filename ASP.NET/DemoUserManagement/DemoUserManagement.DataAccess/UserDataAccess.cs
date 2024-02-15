using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccess
{
    public class UserDataAccess
    {
        public static int SaveUser(UserDetailDTO userDetailDTO)
        {
            using (var context = new UserManagementTableEntities())
            {
                if(userDetailDTO.UserID > 0)
                {
                    UpdateUser(userDetailDTO.UserID, userDetailDTO);
                    return userDetailDTO.UserID;
                }
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
                    FileNameGuid = userDetailDTO.FileNameGuid,
                    FileName = userDetailDTO.FileName,

                };
                context.UserDetails.Add(user);
                context.SaveChanges();

                return user.UserID;

            }
        }

        public static void UpdateUser(int userId, UserDetailDTO userDetailDTO)
        {
            using (UserManagementTableEntities context = new UserManagementTableEntities())
            {
                UserDetail user = context.UserDetails.Find(userId);
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

                    AddressDetail PermanentAddress = user.AddressDetails.FirstOrDefault(a => a.AddressType == 1);
                    if (PermanentAddress != null)
                    {
                        PermanentAddress.UserID = userDetailDTO.UserID;

                    }

                    context.SaveChanges();
                }
            }
        }

        public static void DeleteUser(int userId)
        {
            using (UserManagementTableEntities context = new UserManagementTableEntities())
            {
                UserDetail user = context.UserDetails.FirstOrDefault(x => x.UserID == userId);
                if (user != null)
                {
                    context.UserDetails.Remove(user);
                    context.SaveChanges();
                }
            }
        }

        public static UserDetailDTO GetUserById(int userId)
        {
            using (UserManagementTableEntities context = new UserManagementTableEntities())
            {
                try
                {
                    var userEntity = context.UserDetails.FirstOrDefault(u => u.UserID == userId);

                    if (userEntity != null)
                    {
                        var userDto = new UserDetailDTO
                        {
                            UserID = userEntity.UserID,
                            FirstName = userEntity.FirstName,
                            LastName = userEntity.LastName,
                            Gender = userEntity.Gender,
                            Email = userEntity.Email,
                            PhoneNumber = userEntity.PhoneNumber,
                            DateOfBirth = userEntity.DateOfBirth,
                            FatherName = userEntity.FatherName,
                            MotherName = userEntity.MotherName,
                            FileName = userEntity.FileName,
                            FileNameGuid = userEntity.FileNameGuid,
                        };
                        var presentAddress = userEntity.AddressDetails.FirstOrDefault(a => a.AddressType == 2);
                        if (presentAddress != null)
                        {
                            userDto.PresentAddress = new AddressDetailDTO
                            {
                                Street = presentAddress.Street,
                                City = presentAddress.City,
                                Pincode = presentAddress.Pincode,
                                CountryID = presentAddress.CountryID,
                                StateID = presentAddress.StateID
                            };
                        }

                        var permanentAddress = userEntity.AddressDetails.FirstOrDefault(a => a.AddressType == 1);
                        if (permanentAddress != null)
                        {
                            userDto.PermanentAddress = new AddressDetailDTO
                            {
                                Street = permanentAddress.Street,
                                City = permanentAddress.City,
                                Pincode = permanentAddress.Pincode,
                                CountryID = permanentAddress.CountryID,
                                StateID = permanentAddress.StateID
                            };
                        }


                        return userDto;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    Logger.AddError("GetUserById Failed", ex);
                    throw;
                }
            }
        }

        public static string GetFileNameByFileGuid(string fileNameGuid)
        {
            if (Guid.TryParse(fileNameGuid, out Guid parsedGuid))
            {
                using (UserManagementTableEntities context = new UserManagementTableEntities())
                {
                    UserDetail fileName = context.UserDetails.FirstOrDefault(u => u.FileNameGuid == parsedGuid);

                    return fileName?.FileName;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
