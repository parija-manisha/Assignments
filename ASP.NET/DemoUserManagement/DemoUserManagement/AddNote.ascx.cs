using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Css.Ast;

namespace DemoUserManagement
{
    public partial class AddNote : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string PageName
        {
            get; set;
        }

        protected void SaveNoteButton_Click(object sender, EventArgs e)
        {
            string objectID = Request.QueryString["ObjectID"];
            string noteText = AddNoteText.Text;
            if (string.IsNullOrEmpty(objectID))
            {
                AddingSuccess.Text = "Please the write note id";
                return;
            }

            DataTable dt = ViewState["Note"] as DataTable;
            SaveNoteToDatabase(objectID, noteText);

            AddingSuccess.Text = "Note Added successfully";
        }

        private void SaveNoteToDatabase(string ObjectID, string NoteText)
        {

            using (var connection = Connection.Connect())
            {
                connection.Open();

                string query = "INSERT INTO AddNoteTable VALUES (@objectID, @pageName, @noteText, @date)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@objectID", ObjectID);
                    command.Parameters.AddWithValue("@pageName", PageName);
                    command.Parameters.AddWithValue("@noteText", NoteText);
                    command.Parameters.AddWithValue("@date", DateTime.Now.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}