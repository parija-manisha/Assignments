using DemoUserManagement.DataAccess;
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
        public static void InsertDocument(int objectID, int objectType, int documentName, string fileName, Guid fileNameGuid, string fileExtension)
        {
            DocumentDataAccess.InsertDocument(objectID, objectType, documentName, fileName, fileNameGuid, fileExtension);
        }

        public static DataTable LoadDocument(string objectID, int objectType)
        {
            return DocumentDataAccess.GetDocumentList(objectID, objectType);
        }
    }
}
