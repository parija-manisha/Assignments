using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    [Serializable]
    public class DocumentTypeDTO
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
    }
}
