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
            string file = ConfigurationManager.AppSettings["UploadDocumentPath"] + context.Request.QueryString["file"];
            file = HttpUtility.UrlDecode(file);

            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(file) + "\"");
            context.Response.WriteFile(file);
            context.ApplicationInstance.CompleteRequest();
        }

        public void ProcessRequestOld(HttpContext context)
        {
            string file = ConfigurationManager.AppSettings["MyBasePath"] + context.Request.QueryString["file"];
            file = HttpUtility.UrlDecode(file);

            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                string extension = Path.GetExtension(file).ToLower();

                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(file));
                context.Response.WriteFile(file);

                context.Response.End();

            }
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