using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Model
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public Nullable<int> PhoneNumber { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
