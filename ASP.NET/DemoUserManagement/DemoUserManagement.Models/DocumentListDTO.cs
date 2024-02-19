using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class DocumentListDTO
    {
        public int DocumentID { get; set; }
        public int ObjectID { get; set; }
        public int ObjectType { get; set; }
        public int DocumentType { get; set; }
        public string DocumentNameOnDisk { get; set; }
        public string DocumentOriginalName { get; set; }
    }
}
