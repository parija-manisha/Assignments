using DemoUserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DemoUserManagement.Util
{
    public class Constants
    {
        public struct ObjectType
        {
            public const int UserDetail = 1;
        }


        public struct ObjectIDName
        {
            public const string UserID = "UserID";
        }

        public struct AddressType
        {
            public const int PermanentAddress = 1;
            public const int PresentAddress = 2;
        }

        public static List<DocumentTypeDTO> DocumentType = new List<DocumentTypeDTO> {
            new DocumentTypeDTO { DocumentID = 1, DocumentName = "AadharCard" },
            new DocumentTypeDTO { DocumentID=2, DocumentName="PanCard"},
            new DocumentTypeDTO { DocumentID=3, DocumentName="ProfilePicture"}
            };

        public static UserSession GetSessionDetail()
        {
            return HttpContext.Current.Session["UserSession"] as UserSession;
        }

        public static void SetSessionDetail(UserSession userSession)
        {
            HttpContext.Current.Session["UserSession"] = userSession;
        }

        public static FileSession GetFileSessionDetail()
        {
            return HttpContext.Current.Session["FileSession"] as FileSession;
        }

        public static void SetFileSessionDetail(FileSession fileSession)
        {
            HttpContext.Current.Session["FileSession"] = fileSession;
        }
    }
}
