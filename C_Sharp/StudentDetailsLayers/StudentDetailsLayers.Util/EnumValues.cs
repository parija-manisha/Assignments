using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsLayers.Util
{
    public class EnumValues
    {
        public enum MenuChoiceTable
        {
            Student = 1, Course = 2, Class = 3, StudentClass = 4, StudentCourse = 5, Exit = 6
        }
        public enum MenuChoice
        {
            Insert = 1, Update = 2, Delete = 3, Display = 4
        }
    }
}
