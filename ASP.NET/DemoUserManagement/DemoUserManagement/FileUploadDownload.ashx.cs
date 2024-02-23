using DemoUserManagement.Business;
using DemoUserManagement.Util;
using System;
using System.Configuration;
using System.IO;
using System.Web;

public class FileUploadHandler : IHttpHandler
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
                    //HandleFileDownload(context);
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
        try
        {
            if (TryParseQueryParameters(context, out int userId, out int documentType))
            {
                HttpPostedFile uploadedFile = context.Request.Files["file"];

                if (uploadedFile != null && uploadedFile.ContentLength > 0)
                {
                    string sanitizedFileName = SanitizeFileName(uploadedFile.FileName);
                    Guid fileNameGuid = UploadFileToServer(uploadedFile);

                    if (fileNameGuid != Guid.Empty && userId != 0)
                    {
                        DocumentLogic.InsertDocument(userId, Constants.ObjectType.UserDetail, documentType, sanitizedFileName, fileNameGuid, Path.GetExtension(uploadedFile.FileName));
                    }
                }
            }

            context.Response.Redirect("UserDetails_v2.aspx", false);
            context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Logger.AddError("Error in uploading the file", ex);
        }
    }

    private bool TryParseQueryParameters(HttpContext context, out int userId, out int documentType)
    {
        userId = documentType = 0;

        if (int.TryParse(context.Request.QueryString["UserId"], out userId) &&
            int.TryParse(context.Request.QueryString["DocumentName"], out documentType))
        {
            return true;
        }

        return false;
    }

    private string SanitizeFileName(string originalFileName)
    {
        return Path.GetFileNameWithoutExtension(originalFileName);
    }

    private Guid UploadFileToServer(HttpPostedFile uploadedFile)
    {
        if (uploadedFile != null && uploadedFile.ContentLength > 0)
        {
            try
            {
                string uploadFolderPath = ConfigurationManager.AppSettings["UploadDocumentPath"];

                Guid fileNameGuid = Guid.NewGuid();
                string uniqueFileName = fileNameGuid + Path.GetExtension(uploadedFile.FileName);
                string filePath = Path.Combine(uploadFolderPath, uniqueFileName);
                uploadedFile.SaveAs(filePath);
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
        get { return false; }
    }
}
