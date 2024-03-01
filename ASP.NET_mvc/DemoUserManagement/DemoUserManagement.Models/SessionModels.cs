using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class UserSession
    {
        public int UserId { get; set; }

        public bool IsAdmin { get; set; }

    }

    public class FileSession
    {
        public string fileNameOnDisk { get; set; }
        public string originalFileName { get; set; }
    }
}
