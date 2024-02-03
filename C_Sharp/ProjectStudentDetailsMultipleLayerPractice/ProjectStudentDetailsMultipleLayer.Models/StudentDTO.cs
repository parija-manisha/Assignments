using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStudentDetailsMultipleLayer.Models
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
