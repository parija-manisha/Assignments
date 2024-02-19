

using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;

namespace DemoUserManagement.DataAccess
{
    public class UserDataAccess
    {
        public static int SaveUser(UserDetailDTO userDetailDTO)
        {
            using (var context = new UserManagementTableEntities())
            {
                if (userDetailDTO.UserID > 0)
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
                    Password = userDetailDTO.Password,
                    ConfirmPassword = userDetailDTO.ConfirmPassword,

                };
                context.UserDetails.Add(user);
                context.SaveChanges();

                return user.UserID;

            }
        }

        public static bool IsEmailExists(string email)
        {
            using (var context = new UserManagementTableEntities())
            {
                return context.UserDetails.Any(u => u.Email == email);
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

        public static int GetUserID(string userName, string password)
        {
            int userId = -1;

            using (var connection = Connection.Connect())
            {
                string query = "SELECT UserID FROM UserDetails WHERE Email = @userName AND Password = @password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@password", password);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            userId = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.AddError("Error in GetUserID method", ex);
                    }
                }
            }

            return userId;
        }


        public static void SaveRole(int userId)
        {
            using (var connection = Connection.Connect())
            {
                connection.Open();

                string selectDefaultRolesQuery = "SELECT RoleID FROM Role WHERE isDefaultRole = 'true'";

                using (SqlCommand selectDefaultRolesCommand = new SqlCommand(selectDefaultRolesQuery, connection))
                {
                    using (var reader = selectDefaultRolesCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int roleId = Convert.ToInt32(reader["RoleID"]);

                            using (var insertConnection = Connection.Connect())
                            {
                                insertConnection.Open();

                                string insertUserRoleQuery = "INSERT INTO UserRoles (UserID, RoleID) VALUES (@UserID, @RoleID)";

                                using (SqlCommand insertUserRoleCommand = new SqlCommand(insertUserRoleQuery, insertConnection))
                                {
                                    insertUserRoleCommand.Parameters.AddWithValue("@UserID", userId);
                                    insertUserRoleCommand.Parameters.AddWithValue("@RoleID", roleId);

                                    insertUserRoleCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool IsAdmin(int userId)
        {
            using (var context = new UserManagementTableEntities())
            {
                var isAdmin = context.UserRoles.Any(ur => ur.UserID == userId && ur.Role.isAdmin == "true");

                return isAdmin;
            }
        }

        //public static List<Role> GetRole()
        //{
        //    List<Role> roleList = new List<Role>();
        //    try
        //    {
        //        using (UserManagementTableEntities context = new UserManagementTableEntities())
        //        {
        //            roleList = context.Roles.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.AddError("Couldn't retrieve Country Details", ex);
        //    }
        //    return roleList;
        //}

    }
}
