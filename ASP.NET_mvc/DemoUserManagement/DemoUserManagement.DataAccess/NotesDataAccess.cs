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
        public static void AddNote(string note, int objectId, int objectType)
        {
            try
            {
                if (objectId != -1)
                {
                    using (var context = new UserManagementTableEntities())
                    {
                        var newNote = new Note
                        {
                            ObjectID = objectId,
                            ObjectType = objectType,
                            NoteText = note,
                            TimeStamp = DateTime.Now,
                        };

                        context.Notes.Add(newNote);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Error while inserting into DocumentList", ex);
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
