using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStudentDetailsMultipleLayer.Models
{
    public class StudentCourseDTO
    {
        public int ID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> ClassID { get; set; }
    }
}
