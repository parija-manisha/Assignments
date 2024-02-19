using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DemoUserManagement.Util.Constants;

namespace DemoUserManagement.DataAccess
{
    public class DocumentDataAccess
    {
        public static void InsertDocument(int objectID, int objectType, int documentName, string fileName, Guid fileNameGuid, string fileExtension)
        {
            try
            {
                if (objectID != -1)
                {
                    using (var connection = Connection.Connect())
                    {
                        string documentNameOnDisk = fileNameGuid + fileExtension;
                        connection.Open();
                        string query = "INSERT INTO DocumentList VALUES(@objectID, @objectType, @documentType, @documentNameOnDisk, @DocumentOriginalName)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@objectID", objectID);
                            command.Parameters.AddWithValue("@objectType", objectType);
                            command.Parameters.AddWithValue("@documentType", documentName);
                            command.Parameters.AddWithValue("@documentNameOnDisk", documentNameOnDisk);
                            command.Parameters.AddWithValue("@DocumentOriginalName", fileName);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Fill All the Fields", ex);
            }
        }

        public static string GetFilePathByFileNameGuid(Guid fileNameGuid)
        {
            string uploadFolderPath = ConfigurationManager.AppSettings["UploadDocumentPath"];
            string fileName = fileNameGuid.ToString();
            string fileExtension = ".pdf";

            string filePath = Path.Combine(uploadFolderPath, fileName + fileExtension);

            if (File.Exists(filePath))
            {
                return filePath;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetDocumentDetailsByUserId(int userId)
        {
            using (var context = new UserManagementTableEntities())
            {
                var document = context.DocumentLists.FirstOrDefault(d => d.ObjectID == userId);
                return document != null ? document.DocumentNameOnDisk : null;
            }
        }

    }
}
