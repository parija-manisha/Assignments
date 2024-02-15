using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoUserManagement.Util;
using static DemoUserManagement.Util.Constants;

namespace DemoUserManagement.DataAccess
{
    public class NotesDataAccess
    {
        public static void AddNote(string note, string objectId, int objectType)
        {
            using (var connection = Connection.Connect())
            {
                connection.Open();

                string query = "INSERT INTO Note VALUES (@objectID, @objectType, @noteText, @date)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@objectID", objectId);
                    command.Parameters.AddWithValue("@objectType", objectType);
                    command.Parameters.AddWithValue("@noteText", note);
                    command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));

                    command.ExecuteNonQuery();
                }
            }
        }


        public static DataTable GetNote(string objectID, int objectType)
        {
            DataTable dt = new DataTable();

            using (var connection = Connection.Connect())
            {
                connection.Open();

                string query = "SELECT ObjectID, ObjectType, NoteText, TimeStamp FROM Note WHERE ObjectID = @objectID AND ObjectType = @objectType";
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
