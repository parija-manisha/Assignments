using DemoUserManagement.DataAccess;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.IO;
using System.Web;

namespace DemoUserManagement.Business
{
    public class FileUploadLogic
    {
        public static string GetFileExtension(HttpPostedFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            return fileExtension;

        }

        public static Guid GetFileExtensionGuid(HttpPostedFile file)
        {
            string fileExtension = GetFileExtension(file);
            Guid fileExtensionGuid = FileUploadDataAccess.GetFileExtensionGuid(fileExtension);

            if (fileExtensionGuid == Guid.Empty)
            {
                fileExtensionGuid = Guid.NewGuid();
                FileUploadDataAccess.InsertFileExtension(fileExtension, fileExtensionGuid);
            }

            return fileExtensionGuid;
        }
    }
}
