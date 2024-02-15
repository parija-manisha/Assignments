using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Models
{
    public class NoteDTO
    {
        public int NoteID { get; set; }
        public int UserID { get; set; }
        public int ObjectType { get; set; }
        public string NoteText { get; set; }
        public System.DateTime TimeStamp { get; set; }
    }
}
