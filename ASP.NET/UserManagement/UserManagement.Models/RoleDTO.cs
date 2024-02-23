using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class RoleDTO
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string isDefaultRole { get; set; }
        public string isAdmin { get; set; }
    }
}
