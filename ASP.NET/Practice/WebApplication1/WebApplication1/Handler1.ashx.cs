using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                HttpPostedFile file = context.Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    string uploadDirectory = @"D:\Projects\ASSIGNMENTS\ASP.NET\Upload";

                    string filePath = Path.Combine(uploadDirectory, uniqueFileName);

                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    file.SaveAs(filePath);


                    context.Response.Clear();
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    context.Response.TransmitFile(filePath);
                    context.Response.End();
                }
                else
                {
                    context.Response.Write("No file uploaded.");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
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