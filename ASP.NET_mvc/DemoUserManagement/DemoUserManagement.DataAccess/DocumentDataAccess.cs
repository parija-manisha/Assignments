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
                    using (var context = new UserManagementTableEntities()) 
                    {
                        string documentNameOnDisk = fileNameGuid + fileExtension;

                        var newDocument = new DocumentList
                        {
                            ObjectID = objectID,
                            ObjectType = objectType,
                            DocumentType = documentName,
                            DocumentNameOnDisk = documentNameOnDisk,
                            DocumentOriginalName = fileName
                        };

                        context.DocumentLists.Add(newDocument);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Error while inserting into DocumentList", ex);
            }

        }

        public static DataTable GetDocumentList(string objectID, int objectType)
        {
            DataTable dt = new DataTable();
            using (var connection = Connection.Connect())
            {
                connection.Open();

                string query = "SELECT * FROM DocumentList WHERE ObjectID = @objectID AND ObjectType = @objectType";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@objectID", objectID);
                    command.Parameters.AddWithValue("@objectType", objectType);


                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

    }
}
