using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStudentDetailsMultipleLayer.Models
{
    public class CourseClassDTO
    {
        public int ID { get; set; }
        public Nullable<int> CourseID { get; set; }
        public Nullable<int> ClassID { get; set; }
    }
}
