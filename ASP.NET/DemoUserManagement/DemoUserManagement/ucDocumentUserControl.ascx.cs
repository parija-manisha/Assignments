using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class ucDocumentUserControl : System.Web.UI.UserControl
    {
        public int ObjectType
        {
            get; set;

        }

        public string ObjectIDName
        {
            get; set;
        }

        public string FileName
        {
            get { return ViewState["DocumentFileName"] as string ?? string.Empty; }
            set { ViewState["DocumentFileName"] = value; }
        }

        public Guid FileNameGuid
        {
            get { return (Guid)(ViewState["DocumentExtension"] ?? Guid.Empty); }
            set { ViewState["DocumentExtension"] = value; }
        }

        public List<DocumentTypeDTO> DocumentType
        {
            get
            {
                if (ViewState["Documents"] == null)
                {
                    ViewState["Documents"] = new List<DocumentTypeDTO>();
                }
                return (List<DocumentTypeDTO>)ViewState["Documents"];
            }
            set { ViewState["Documents"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDocumentType();
            }
        }

        private void BindDocumentType()
        {
            DocumentDropDown.DataSource = Constants.DocumentType;
            DocumentDropDown.DataValueField = "DocumentID";
            DocumentDropDown.DataTextField = "DocumentName";
            DocumentDropDown.DataBind();
        }

        protected void DocumentDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedDocumentID = Convert.ToInt32(DocumentDropDown.SelectedValue);
            DocumentTypeDTO selectedDocument = DocumentType.Find(doc => doc.DocumentID == selectedDocumentID);

            if (selectedDocument != null)
            {
                int documentID = selectedDocument.DocumentID;
                string documentName = selectedDocument.DocumentName;

            }
        }

        //public int GetSelectedDocumentId()
        //{
        //    if (DocumentType != null)
        //    {
        //        int selectedDocumentID = Convert.ToInt32(DocumentDropDown.SelectedValue);
        //        DocumentTypeDTO selectedDocument = DocumentType.Find(doc => doc.DocumentID == selectedDocumentID);

        //        return selectedDocument != null ? selectedDocument.DocumentID : 1;
        //    }

        //    return 1;
        //}
    }
}