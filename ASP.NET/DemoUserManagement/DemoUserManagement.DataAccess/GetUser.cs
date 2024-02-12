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
    public class GetUser
    {
        public List<UserDetailDTO> Users()
        {
            List<UserDetailDTO> users = new List<UserDetailDTO>();
            using (var connection = Connection.Connect())
            {
                string query = "SELECT * FROM UserDetails";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserDetailDTO user = MapUserFromDataReader(reader);
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        private UserDetailDTO MapUserFromDataReader(SqlDataReader reader)
        {
            UserDetailDTO user = new UserDetailDTO
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                FirstName = reader["FirstName"].ToString(),
                MiddleName = reader["MiddleName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Gender = reader["Gender"].ToString(),
                Email = reader["Email"].ToString(),
                PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? (int?)null : (int?)reader["PhoneNumber"],
                DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["DateOfBirth"],
                FatherName = reader["FatherName"].ToString(),
                MotherName = reader["MotherName"].ToString()
            };

            return user;
        }
    }
}
