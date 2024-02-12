using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class Users : System.Web.UI.Page
    {
        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? string.Empty; }
            set { ViewState["SortExpression"] = value; }
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();

                if (Session["SelectedUserID"] != null)
                {
                    int selectedUserID;
                    if (int.TryParse(Session["SelectedUserID"].ToString(), out selectedUserID))
                    {
                        AddNote addNoteControl = LoadControl("~/AddNote.ascx") as AddNote;

                        //AddNotePlaceholder.Controls.Add(addNoteControl);

                        Session.Remove("SelectedUserID");
                    }
                }
            }
        }

        private void BindGridView()
        {
            UserLogic userLogic = new UserLogic();
            var userList = userLogic.GetAllUsers();


            GridViewUsers.DataSource = userList;
            GridViewUsers.DataBind();
        }

        protected void GridViewUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsers.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void GridViewUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                if (e.NewEditIndex >= 0 && e.NewEditIndex < GridViewUsers.Rows.Count)
                {
                    string userId = GridViewUsers.DataKeys[e.NewEditIndex]["UserID"].ToString();
                    int.TryParse(userId, out int id);
                    UserDetailDTO userDetailDTO = new UserDetailDTO();
                    UserLogic.UpdateUser(id, userDetailDTO);
                    Session["SelectedUserID"] = id;
                    Response.Redirect($"UserDetails.aspx?UserId={userId}");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Could not retrieve ID", ex);
            }
        }

        protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void GridViewUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortExpression = e.SortExpression;
            SortDirection = GetSortDirection(SortExpression);

            BindGridView();
        }

        private string GetSortDirection(string column)
        {
            if (column == SortExpression)
            {
                return SortDirection == "ASC" ? "DESC" : "ASC";
            }

            return "ASC";
        }
    }
}
