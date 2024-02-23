using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class SessionModel
    {
        public int UserId { get; set; }

        public bool IsAdmin { get; set; }
    }
}
