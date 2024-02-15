using DemoUserManagement.DataAccess;
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
    public class FileUploadDataAccess
    {
        public static Guid GetFileExtensionGuid(string fileExtension)
        {
            Guid guid = Guid.Empty;
            using (var connection = Connection.Connect())
            {
                string query = "SELECT FileExtensionGuid FROM FileUpload WHERE FileExtension = @FileExtension";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FileExtension", fileExtension);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            guid = (Guid)result;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.AddError($"Error while retrieving FileExtensionGuid", ex);
                    }
                }
            }

            return guid;
        }

        public static void InsertFileExtension(string fileExtension, Guid fileExtensionGuid)
        {

            using (var connection = Connection.Connect())
            {
                connection.Open();

                string query = "INSERT INTO FileExtension (FileExtension, FileExtensionGuid) VALUES (@FileExtension, @FileExtensionGuid)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FileExtension", fileExtension);
                    command.Parameters.AddWithValue("@FileExtensionGuid", fileExtensionGuid);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
