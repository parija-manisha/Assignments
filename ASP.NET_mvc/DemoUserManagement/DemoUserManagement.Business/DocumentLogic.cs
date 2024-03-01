using DemoUserManagement.DataAccess;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static DemoUserManagement.Util.Constants;

namespace DemoUserManagement.Business
{
    public class DocumentLogic
    {
        public static void InsertDocument(int objectID, int objectType, int documentName, string fileName, string fileNameGuid)
        {
            DocumentDataAccess.InsertDocument(objectID, objectType, documentName, fileName, fileNameGuid);
        }

        public static List<DocumentList> LoadDocument(int objectID, int objectType)
        {
            return DocumentDataAccess.GetDocumentList(objectID, objectType);
        }

        public static int CountDocument(int userId, int objectType)
        {
            return DocumentDataAccess.CountDocument(userId, objectType);
        }

        public static DocumentListDTO GetDocumentDetails(int documentId)
        {
            return DocumentDataAccess.GetDocumentDetails(documentId);
        }
    }
}
