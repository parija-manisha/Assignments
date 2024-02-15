using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using WebGrease.Css.Ast;
using DemoUserManagement.Business;

namespace DemoUserManagement
{
    public partial class ucNoteControl : System.Web.UI.UserControl
    {
        public int ObjectType
        {
            get; set;

        }
      
        public string ObjectIDName
        {
            get; set;
        }
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadExistingNotes();
            }
        }
        
        protected void LoadExistingNotes()
        {
            string objectID = Request.QueryString[ObjectIDName];

            if (!string.IsNullOrEmpty(objectID))
            {
                DataTable dt = NoteLogic.LoadNotes(objectID, ObjectType);
                ViewState["Note"] = dt;
                this.BindGrid();
            }
        }
        
        protected void SaveNoteButton_Click(object sender, EventArgs e)
        {
            string objectID = Request.QueryString[ObjectIDName];
            string noteText = AddNoteText.Text;
            if (string.IsNullOrEmpty(objectID))
            {
                AddingSuccess.Text = "Please the write note id";
                return;
            }

            DataTable dt = ViewState["Note"] as DataTable;
            if (dt == null)
            {
                dt = new DataTable(); 
                dt.Columns.Add("ObjectID");
                dt.Columns.Add("ObjectType");
                dt.Columns.Add("NoteText");
                dt.Columns.Add("TimeStamp");
            }

            NoteLogic.AddNote(noteText, objectID, ObjectType);

            dt.Rows.Add(objectID, ObjectType, noteText, DateTime.Now.ToString());
            ViewState["Note"] = dt;
            this.BindGrid();
            AddNoteText.Text = string.Empty;

            AddingSuccess.Text = "Note Added successfully";
        }
        
        protected void BindGrid()
        {
            int currentPageIndex = NoteListGridView.PageIndex;
            int pageSize = NoteListGridView.PageSize;
            int totalCount = GetTotalCount();

            NoteListGridView.VirtualItemCount = totalCount;
            GetPageData(currentPageIndex, pageSize);

            DataTable dt = ViewState["Note"] as DataTable;

            NoteListGridView.DataSource = dt.AsEnumerable()
       .Select(r => new
       {
           ObjectID = r["ObjectID"],
           ObjectType = r["ObjectType"],
           NoteText = r["NoteText"],
           TimeStamp = DateTime.Parse(r["TimeStamp"].ToString()).ToString("dd-MM-yyyy hh:mm:ss tt")
       })
       .ToList();

            NoteListGridView.DataBind();
        }

        private void GetPageData(int currentPageIndex, int pageSize)
        {
            string objectID = Request.QueryString[ObjectIDName];
            using (var connection = Connection.Connect())
            {
                string sortExpression = ViewState["SortExpression"] != null ? ViewState["SortExpression"].ToString() : "ObjectID";
                string sortDirection = ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC";


                string query = string.Format(@"SELECT * FROM (
                                SELECT ROW_NUMBER() OVER (ORDER BY {0} {1}) AS RowNum, * 
                                FROM Note
                                WHERE ObjectID = @ObjectID AND ObjectType = @ObjectType
                            ) AS Notes 
                            WHERE RowNum BETWEEN @StartIndex AND @EndIndex", sortExpression, sortDirection);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int startIndex = currentPageIndex * pageSize + 1;
                    int endIndex = startIndex + pageSize - 1;

                    command.Parameters.AddWithValue("@StartIndex", startIndex);
                    command.Parameters.AddWithValue("@EndIndex", endIndex);
                    command.Parameters.AddWithValue("@ObjectID", objectID);
                    command.Parameters.AddWithValue("@ObjectType", ObjectType);

                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        NoteListGridView.DataSource = dt;
                        NoteListGridView.DataBind();
                    }
                }

            }
        }
       
        private int GetTotalCount()
        {
            string objectID = Request.QueryString[ObjectIDName];
            using (var connection = Connection.Connect())
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Note  WHERE ObjectID = @ObjectID AND ObjectType = @ObjectType", connection))
                {
                    command.Parameters.AddWithValue("@ObjectID", objectID);
                    command.Parameters.AddWithValue("@ObjectType", ObjectType);
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }

        protected void NoteListGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            NoteListGridView.PageIndex = e.NewPageIndex;
        }

        protected void NoteListGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortDirection = "ASC";
            if (ViewState["SortDirection"] != null)
            {
                sortDirection = ViewState["SortDirection"].ToString();
                if (e.SortExpression == ViewState["SortExpression"].ToString())
                {
                    sortDirection = (sortDirection == "ASC") ? "DESC" : "ASC";
                }
            }

            ViewState["SortExpression"] = e.SortExpression;
            ViewState["SortDirection"] = sortDirection;

        }

        protected void NoteListGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowNumber = e.Row.RowIndex + 1;
                Label lblSlNo = (Label)e.Row.FindControl("lblSlNo");

                if (lblSlNo != null)
                {
                    lblSlNo.Text = rowNumber.ToString();
                }
            }
        }
    }
}