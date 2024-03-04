using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
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
                    DateOfBirth = userDetailDTO.DateOfBirth.Date,
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

        public static void SaveAddress(int userId, UserDetailDTO userDetailDTO)
        {
            using (var context = new UserManagementTableEntities())
            {
                AddressDetail permanentAddress = new AddressDetail
                {
                    UserID = userId,
                    AddressType = Constants.AddressType.PermanentAddress,
                    CountryID = userDetailDTO.PermanentAddress.CountryID,
                    StateID = userDetailDTO.PermanentAddress.StateID,
                    City = userDetailDTO.PermanentAddress.City,
                    Pincode = userDetailDTO.PermanentAddress.Pincode,
                    Street = userDetailDTO.PermanentAddress.Street
                };

                AddressDetail presentAddress = new AddressDetail
                {
                    UserID = userId,
                    AddressType = Constants.AddressType.PresentAddress,
                    CountryID = userDetailDTO.PresentAddress.CountryID,
                    StateID = userDetailDTO.PresentAddress.StateID,
                    City = userDetailDTO.PresentAddress.City,
                    Pincode = userDetailDTO.PresentAddress.Pincode,
                    Street = userDetailDTO.PresentAddress.Street
                };

                context.AddressDetails.Add(permanentAddress);
                context.AddressDetails.Add(presentAddress);

                context.SaveChanges();
            }

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

        public static List<UserDetailDTO> GetAllUsers(int start, int length, string sortColumn, string sortDirection)
        {
            using (var context = new UserManagementTableEntities())
            {
                var query = from user in context.UserDetails
                            select new UserDetailDTO
                            {
                                UserID = user.UserID,
                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,
                                LastName = user.LastName,
                                Gender = user.Gender,
                                PhoneNumber = user.PhoneNumber,
                                DateOfBirth = user.DateOfBirth,
                                Email = user.Email,
                                FatherName = user.FatherName,
                                MotherName = user.MotherName,
                            };

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
                {
                    query = ApplySorting(query, sortColumn, sortDirection);
                }

                query = query.Skip(start).Take(length);
                return query.ToList();
            }
        }

        private static IQueryable<UserDetailDTO> ApplySorting(IQueryable<UserDetailDTO> query, string sortColumn, string sortDirection)
        {
            switch (sortColumn)
            {
                case "Name":
                    query = (sortDirection == "asc") ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName);
                    break;
                case "DOB":
                    query = (sortDirection == "asc") ? query.OrderBy(u => u.DateOfBirth) : query.OrderByDescending(u => u.DateOfBirth);
                    break;
            }

            return query;
        }

        public static int GetTotalRecords()
        {
            using (var context = new UserManagementTableEntities())
            {
                return context.UserDetails.Count();
            }
        }

        public static int GetFilteredRecords(string sortColumn, string sortDirection)
        {
            using (var context = new UserManagementTableEntities())
            {
                var query = from user in context.UserDetails
                            select new UserDetailDTO
                            {
                                UserID = user.UserID,
                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,
                                LastName = user.LastName,
                                Gender = user.Gender,
                                PhoneNumber = user.PhoneNumber,
                                DateOfBirth = user.DateOfBirth,
                                Email = user.Email,
                                FatherName = user.FatherName,
                                MotherName = user.MotherName,
                            };

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
                {
                    query = ApplySorting(query, sortColumn, sortDirection);
                }

                return query.Count();
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
                    user.Password = userDetailDTO.Password;
                    user.ConfirmPassword = userDetailDTO.ConfirmPassword;
                    user.PhoneNumber = userDetailDTO.PhoneNumber;
                    user.DateOfBirth = userDetailDTO.DateOfBirth;
                    user.FatherName = userDetailDTO.FatherName;
                    user.MotherName = userDetailDTO.MotherName;

                    AddressDetail PermanentAddress = user.AddressDetails.FirstOrDefault(a => a.AddressType == 1);
                    if (PermanentAddress != null)
                    {
                        PermanentAddress.UserID = userDetailDTO.UserID;
                        PermanentAddress.Street = userDetailDTO.PermanentAddress.Street;
                        PermanentAddress.City = userDetailDTO.PermanentAddress.City;
                        PermanentAddress.StateID = userDetailDTO.PermanentAddress.StateID;
                        PermanentAddress.CountryID = userDetailDTO.PermanentAddress.CountryID;
                        PermanentAddress.Pincode = userDetailDTO.PermanentAddress.Pincode;
                    }

                    AddressDetail PresentAddress = user.AddressDetails.FirstOrDefault(a => a.AddressType == 2);
                    if (PresentAddress != null)
                    {
                        PresentAddress.UserID = userDetailDTO.UserID;
                        PresentAddress.Street = userDetailDTO.PresentAddress.Street;
                        PresentAddress.City = userDetailDTO.PresentAddress.City;
                        PresentAddress.StateID = userDetailDTO.PresentAddress.StateID;
                        PresentAddress.CountryID = userDetailDTO.PresentAddress.CountryID;
                        PresentAddress.Pincode = userDetailDTO.PresentAddress.Pincode;
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
                    var userEntity = context.UserDetails
                        .FirstOrDefault(u => u.UserID == userId);

                    if (userEntity != null)
                    {
                        var userDto = new UserDetailDTO
                        {
                            UserID = userEntity.UserID,
                            FirstName = userEntity.FirstName,
                            MiddleName = userEntity.LastName,
                            LastName = userEntity.LastName,
                            Gender = userEntity.Gender,
                            Email = userEntity.Email,
                            PhoneNumber = userEntity.PhoneNumber,
                            Password = userEntity.Password,
                            ConfirmPassword = userEntity.ConfirmPassword,
                            DateOfBirth = userEntity.DateOfBirth,
                            FatherName = userEntity.FatherName,
                            MotherName = userEntity.MotherName,
                            Countries = new List<CountryDTO>(),
                            States = new List<StateDTO>(),
                            Roles = new List<RoleDTO>(),
                            DocumentType = new List<DocumentTypeDTO>()
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

                            userDto.Countries.Add(new CountryDTO
                            {
                                CountryID = presentAddress.CountryID,
                                CountryName = presentAddress.Country.CountryName
                            });

                            userDto.States.Add(new StateDTO
                            {
                                StateID = presentAddress.StateID,
                                StateName = presentAddress.State.StateName
                            });
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

                            userDto.Countries.Add(new CountryDTO
                            {
                                CountryID = permanentAddress.CountryID,
                                CountryName = permanentAddress.Country.CountryName
                            });

                            userDto.States.Add(new StateDTO
                            {
                                StateID = permanentAddress.StateID,
                                StateName = permanentAddress.State.StateName
                            });
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

        public static bool IsAdmin(int userId)
        {
            using (var context = new UserManagementTableEntities())
            {
                var isAdmin = context.UserRoles.Any(ur => ur.UserID == userId && ur.Role.isAdmin == "true");

                return isAdmin;
            }
        }

        public static List<Role> GetRole()
        {
            List<Role> roleList = new List<Role>();
            try
            {
                using (UserManagementTableEntities context = new UserManagementTableEntities())
                {
                    roleList = context.Roles.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Couldn't retrieve Role Details", ex);
            }
            return roleList;
        }

    }
}
