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
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ObjectType { get; set; }
        public string NoteText { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
    }
}
