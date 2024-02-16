using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private List<DocumentTypeDTO> Documents
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

        public void UploadFile(int userID)
        {
            int objectID = userID;

            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                try
                {
                    DocumentTypeDTO selectedDocument = new DocumentTypeDTO
                    {
                        DocumentID = Convert.ToInt32(DocumentDropDown.SelectedValue),
                        DocumentName = DocumentDropDown.SelectedValue,
                    };

                    string fileExtension = Path.GetExtension(DdlFileUpload.PostedFile.FileName);

                    Documents.Add(selectedDocument);

                    string documentName = selectedDocument.DocumentName;
                    DocumentLogic.InsertDocument(objectID, ObjectType, documentName, FileName, FileNameGuid, fileExtension);

                    ErrorMessage.Text = "";
                }
                catch (Exception ex)
                {
                    Logger.AddError("Error: ", ex);
                    ErrorMessage.Text = "An error occurred while uploading the file.";
                }
            }
            else
            {
                ErrorMessage.Text = "Please select a file to upload";
            }
        }

        public void LoadDocumentDetails()
        {
            FileUploadDownload fileUploadDownload = new FileUploadDownload();
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                FileNameGuid = fileUploadDownload.UploadFileToServer(DdlFileUpload.PostedFile);
                FileName = Path.GetFileName(DdlFileUpload.PostedFile.FileName);
            }
            else
            {
                FileNameGuid = Guid.Empty;
                FileName = string.Empty;
            }
        }

        protected void DocumentDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedDocumentID = Convert.ToInt32(DocumentDropDown.SelectedValue);

            DocumentTypeDTO selectedDocument = Documents.Find(doc => doc.DocumentID == selectedDocumentID);

            if (selectedDocument != null)
            {
                int documentID = selectedDocument.DocumentID;
                string documentName = selectedDocument.DocumentName;
            }
        }
    }
}