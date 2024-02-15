﻿using DemoUserManagement.DataAccess;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
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
        public static void InsertDocument(int objectID, int objectType, string documentName, string fileName, Guid fileNameGuid, string fileExtension)
        {
            DocumentDataAccess.InsertDocument(objectID, objectType, documentName, fileName, fileNameGuid, fileExtension);
        }
    }
}