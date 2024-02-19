using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using static DemoUserManagement.Util.Constants;

namespace DemoUserManagement
{
    /// <summary>
    /// Summary description for FileUploadDownload
    /// </summary>
    public class FileUploadDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["Action"] != null)
            {
                string action = context.Request.QueryString["Action"].ToLower();

                switch (action)
                {
                    case "upload":
                        HandleFileUpload(context);
                        break;

                    case "download":
                        HandleFileDownload(context);
                        break;

                    default:
                        context.Response.Write("Invalid action specified.");
                        break;
                }
            }
            else
            {
                context.Response.Write("No action specified.");
            }
        }

        private void HandleFileUpload(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                if (int.TryParse(context.Request.QueryString["UserID"], out int userId))
                {

                    int objectID = userId;
                    int objectType = ObjectType.UserDetail;
                    int documentName = 1;

                    HttpPostedFile uploadedFile = context.Request.Files[0];

                    string fileExtension = Path.GetExtension(uploadedFile.FileName);
                    Guid fileNameGuid = UploadFileToServer(uploadedFile);

                    if (fileNameGuid != Guid.Empty)
                    {
                        DocumentLogic.InsertDocument(objectID, objectType, documentName, uploadedFile.FileName, fileNameGuid, fileExtension);

                        context.Response.Write("File uploaded successfully. FileNameGuid: " + fileNameGuid);
                    }
                    else
                    {
                        context.Response.Write("Error uploading file.");
                    }
                }
            }
            else
            {
                context.Response.Write("No file uploaded.");
            }
        }


        private void HandleFileDownload(HttpContext context)
        {
            if (context.Request.QueryString["UserID"] != null)
            {
                if (int.TryParse(context.Request.QueryString["UserID"], out int userId))
                {
                    string documentNameOnDisk = DocumentLogic.GetDocumentDetailsByUserId(userId);

                    if (!string.IsNullOrEmpty(documentNameOnDisk))
                    {
                        string filePath = Path.Combine(ConfigurationManager.AppSettings["UploadDocumentPath"], documentNameOnDisk);

                        if (File.Exists(filePath))
                        {
                            context.Response.ContentType = "application/octet-stream";
                            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                            context.Response.TransmitFile(filePath);
                            context.Response.End();
                        }
                        else
                        {
                            context.Response.Write("File Not Found");
                        }
                    }
                    else
                    {
                        context.Response.Write("Document details not found for the specified UserID");
                    }

                }
                else
                {
                    context.Response.Write("Document details not found for the specified UserID");
                }
            }
            else
            {
                context.Response.Write("Invalid UserID");
            }
        }


        public Guid UploadFileToServer(HttpPostedFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.ContentLength > 0)
            {
                try
                {
                    string uploadFolderPath = ConfigurationManager.AppSettings["UploadDocumentPath"];

                    Guid fileNameGuid = Guid.NewGuid();
                    string FileExtension = Path.GetExtension(uploadedFile.FileName);
                    string UniqueFileName = fileNameGuid + FileExtension;
                    string FilePath = Path.Combine(uploadFolderPath, UniqueFileName);
                    uploadedFile.SaveAs(FilePath);
                    return fileNameGuid;
                }
                catch (Exception ex)
                {
                    Logger.AddError("Error in uploading the file", ex);
                    return Guid.Empty;
                }
            }
            return Guid.Empty;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
