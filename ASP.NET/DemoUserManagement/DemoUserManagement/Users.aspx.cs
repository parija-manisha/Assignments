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
                UserDetailDTO user = UserLogic.GetUserById(userId);

                if (user != null && user.FileNameGuid != Guid.Empty)
                {
                    string fileNameGuidString = user.FileNameGuid.ToString();
                    string fileName = user.FileName;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        string filePath = Server.MapPath("~/upload/" + fileNameGuidString + Path.GetExtension(fileName));
                        if (File.Exists(filePath))
                        {
                            Response.Clear();
                            Response.ContentType = "application/octet-stream";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.TransmitFile(filePath);
                            Response.End();
                        }
                        else
                        {
                            SuccessMessage.Text = "File Not Found";
                        }
                    }
                    else
                    {
                        SuccessMessage.Text = "File Not Found in Database";
                    }
                }
                else
                {
                    SuccessMessage.Text = "User or File Not Found";
                }
            }
            else
            {
                SuccessMessage.Text = "Invalid User Identifier";
            }
        }
    }
}