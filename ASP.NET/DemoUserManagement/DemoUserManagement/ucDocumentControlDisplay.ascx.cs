using DemoUserManagement.Business;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class ucDocumentUserControlDisplay : System.Web.UI.UserControl
    {
       // public int ObjectType
       // {
       //     get; set;

       // }

       // public string ObjectIDName
       // {
       //     get; set;
       // }

       // public string DocumentType
       // {
       //     get; set;
       // }

       // protected void Page_Load(object sender, EventArgs e)
       // {
       //     if (!this.IsPostBack)
       //     {
       //         LoadExistingDocuments();
       //     }
       // }

       // protected void LoadExistingDocuments()
       // {
       //     string objectID = Request.QueryString[ObjectIDName];

       //     if (!string.IsNullOrEmpty(objectID))
       //     {
       //         DataTable dt = DocumentLogic.LoadDocument(objectID, ObjectType);
       //         ViewState["Document"] = dt;
       //         this.BindGrid();
       //     }
       // }

       // protected void BindGrid()
       // {
       //     DataTable dt = ViewState["Note"] as DataTable;

       //     DocumentListGridView.DataSource = dt.AsEnumerable()
       //.Select(r => new
       //{
       //    ObjectID = r["ObjectID"],
       //    ObjectType = r["ObjectType"],
       //    DocumentType = r["DocumentType"],
       //    DocumentNameOnDisk = r["DocumentNameOnDisk"],
       //    OriginalDocumentName = r["OriginalDocumentName"]
       //})
       //.ToList();

       //     DocumentListGridView.DataBind();
       // }

       // protected void NoteListGridView_RowDataBound(object sender, GridViewRowEventArgs e)
       // {
       //     if (e.Row.RowType == DataControlRowType.DataRow)
       //     {
       //         int rowNumber = e.Row.RowIndex + 1;
       //         Label lblSlNo = (Label)e.Row.FindControl("lblSlNo");

       //         if (lblSlNo != null)
       //         {
       //             lblSlNo.Text = rowNumber.ToString();
       //         }
       //     }
       // }
    }
}