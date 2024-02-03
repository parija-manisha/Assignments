using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Model
{
    public class CourseClassDTO
    {
        public int ID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> CourseID { get; set; }
    }
}
