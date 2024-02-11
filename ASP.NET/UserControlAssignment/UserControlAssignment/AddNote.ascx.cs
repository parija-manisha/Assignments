using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using UserControlAssignment.Util;

namespace UserControlAssignment
{
    public partial class AddNote : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadExistingNotes();
            }
        }

        public string PageName
        {
            get; set;
        }

        protected void BindGrid()
        {
            NoteListGridView.DataSource = ViewState["Note"] as DataTable;
            NoteListGridView.DataBind();
        }

        protected void LoadExistingNotes()
        {
            string objectID = Request.QueryString["ObjectID"];

            if (!string.IsNullOrEmpty(objectID))
            {
                DataTable dt = LoadNotesFromDatabase(objectID, PageName);
                ViewState["Note"] = dt;
                this.BindGrid();
            }
        }

        private DataTable LoadNotesFromDatabase(string objectID, string Page)
        {
            DataTable dt = new DataTable();

            using (var connection = Connection.Connect())
            {
                connection.Open();

                string query = "SELECT ObjectID, PageName, Text, Date FROM AddNoteTable WHERE ObjectID = @objectID AND PageName = @Page";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ObjectID", objectID);
                    command.Parameters.AddWithValue("@Page", Page);


                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
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

            dt.Rows.Add(objectID, PageName, noteText, DateTime.Now.ToString());
            ViewState["Note"] = dt;
            this.BindGrid();
            AddNoteText.Text = string.Empty;


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