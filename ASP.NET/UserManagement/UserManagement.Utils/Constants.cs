using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UserManagement.Models;

namespace UserManagement.Utils
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

        public static void SetSessionDetail(UserSession sessionModel)
        {
            HttpContext.Current.Session["UserSession"] = sessionModel;
        }
    }
}
