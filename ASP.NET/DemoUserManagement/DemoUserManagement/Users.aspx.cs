using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class Users : BasePage
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
            }
        }

        private void BindGridView()
        {
            var userList = UserLogic.GetAllUsers();
            GridViewUsers.DataSource = userList;
            GridViewUsers.DataBind();

        }

        protected void GridViewUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsers.PageIndex = e.NewPageIndex;
            BindGridView();
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

        protected void LinkButtonDownload_Command(object sender, CommandEventArgs e)
        {
            string userIdString = e.CommandArgument.ToString();

            if (int.TryParse(userIdString, out int userId))
            {
                string fileHandlerUrl = "FileUploadDownload.ashx?Action=download&UserID=" + userId;
                Response.Redirect(fileHandlerUrl);
            }
            else
            {
                SuccessMessage.Text = "Invalid User Identifier";
            }
        }
    }
}