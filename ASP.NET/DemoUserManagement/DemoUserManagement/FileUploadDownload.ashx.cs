using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace DemoUserManagement
{
    /// <summary>
    /// Summary description for FileUploadDownload
    /// </summary>
    public class FileUploadDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/octet-stream";

            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile uploadedFile = context.Request.Files[0];

                string fileExtension = Path.GetExtension(uploadedFile.FileName);
                string uploadedFileName = UploadFileToServer(uploadedFile) + fileExtension;

                if (!string.IsNullOrEmpty(uploadedFileName))
                {
                    context.Response.Write("File uploaded successfully. FileName: " + uploadedFileName);
                }
                else
                {
                    context.Response.Write("Error uploading file.");
                }
            }
            else
            {
                context.Response.Write("No file uploaded.");
            }
        }

        public Guid UploadFileToServer(HttpPostedFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.ContentLength > 0)
            {
                try
                {
                    string uploadFolderPath = ConfigurationManager.AppSettings["UploadDocumentPath"];

                    string FileName = Path.GetFileName(uploadedFile.FileName);
                    Guid FileNameGuid = Guid.NewGuid();
                    string FileExtension = Path.GetExtension(uploadedFile.FileName);
                    string UniqueFileName = FileNameGuid + FileExtension;
                    string FilePath = Path.Combine(uploadFolderPath, UniqueFileName);
                    uploadedFile.SaveAs(FilePath);
                    return FileNameGuid;
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