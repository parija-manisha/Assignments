using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class FileUploadDTO
    {
        public int FileID { get; set; }
        public string FileExtension { get; set; }
        public System.Guid FileExtensionGuid { get; set; } = System.Guid.NewGuid();
    }
}
