using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoUserManagement.Util;

namespace DemoUserManagement.DataAccess
{
    public class NotesDataAccess
    {
        public void AddNote(string note, string userId, string objectType, DateTime dateTime)
        {
            using (var connection = Connection.Connect())
            {
                connection.Open();

                string query = "INSERT INTO Notes (UserID, ObjectType, NoteText, TimeStamp) VALUES (@UserId, @ObjectType,@NoteText, @DateTime)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ObjectType", objectType);
                    command.Parameters.AddWithValue("@Note", note);
                    command.Parameters.AddWithValue("@DateTime", dateTime);

                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetNotes(int pageIndex, int pageSize)
        {
            DataTable dataTable = new DataTable();

            using (var connection = Connection.Connect())
            {
                int startRowIndex = (pageIndex - 1) * pageSize + 1;
                int endRowIndex = pageIndex * pageSize;

                string query = $@"SELECT * FROM (
                SELECT ROW_NUMBER() OVER (ORDER BY NoteID) AS NoteID, UserID, ObjectType, NoteText, TimeStamp 
                FROM Notes) AS NotesPage WHERE NoteID BETWEEN @StartRowIndex AND @EndRowIndex";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                    command.Parameters.AddWithValue("@EndRowIndex", endRowIndex);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
    }
}
