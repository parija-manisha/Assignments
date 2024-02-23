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
   
        public static DataTable GetDocument(string objectID, int objectType)
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
