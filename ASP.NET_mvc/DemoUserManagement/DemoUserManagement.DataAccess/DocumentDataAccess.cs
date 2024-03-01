using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static DemoUserManagement.Util.Constants;

namespace DemoUserManagement.DataAccess
{
    public class DocumentDataAccess
    {
        public static void InsertDocument(int objectID, int objectType, int documentName, string fileName, string fileNameGuid)
        {
            try
            {
                if (objectID != -1)
                {
                    using (var context = new UserManagementTableEntities()) 
                    {
                        var newDocument = new DocumentList
                        {
                            ObjectID = objectID,
                            ObjectType = objectType,
                            DocumentType = documentName,
                            DocumentNameOnDisk = fileNameGuid,
                            DocumentOriginalName = fileName
                        };

                        context.DocumentLists.Add(newDocument);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Error while inserting into DocumentList", ex);
            }

        }

        public static List<DocumentList> GetDocumentList(int objectID, int objectType)
        {
            try
            {
                using (var context = new UserManagementTableEntities())
                {
                    var documents = context.DocumentLists
                        .Where(n => n.ObjectID == objectID && n.ObjectType == objectType)
                        .ToList();

                    return documents;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Error while retrieving notes", ex);
                return new List<DocumentList>();
            }
        }

        public static int CountDocument(int userId, int objectType)
        {
            using (var context = new UserManagementTableEntities())
            {
                int count = context.DocumentLists.Where(n => n.ObjectID == userId && n.ObjectType == objectType).Count();

                return count;
            }
        }

        public static DocumentListDTO GetDocumentDetails(int documentId)
        {
            using (UserManagementTableEntities context = new UserManagementTableEntities())
            {
                try
                {
                    var document = context.DocumentLists.FirstOrDefault(u => u.DocumentID == documentId);

                    if (document != null)
                    {
                        var documentDto = new DocumentListDTO
                        {
                           ObjectID = document.ObjectID,
                           ObjectType= document.ObjectType,
                           DocumentType= document.DocumentType,
                           DocumentNameOnDisk= document.DocumentNameOnDisk,
                           DocumentOriginalName= document.DocumentOriginalName,
                        };
                        return documentDto;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    Logger.AddError("Failed", ex);
                    throw;
                }
            }

        }

        public static object UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName = Guid.NewGuid().ToString("N") + fileExtension;

                    string uploadPath = ConfigurationManager.AppSettings["UploadDocumentPath"];
                    string filePath = Path.Combine(uploadPath, fileName);

                    file.SaveAs(filePath);

                    var response = new
                    {
                        success = true,
                        message = "File uploaded successfully",
                    };

                    FileSession fileSession = new FileSession();
                    fileSession.originalFileName = file.FileName;
                    fileSession.fileNameOnDisk = fileName;

                    Constants.SetFileSessionDetail(fileSession);

                    return response;
                }
                else
                {
                    var response = new
                    {
                        success = false,
                        message = "Please select a file to upload."
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    success = false,
                    message = "Error uploading file: " + ex.Message
                };

                return response;
            }
        }
    }
}
