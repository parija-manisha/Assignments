using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Util
{
    public class EnumValues
    {
        public enum MenuChoiceTable
        {
            Student = 1, Course = 2, Class = 3, StudentClass = 4, CourseClass = 5, Exit = 6
        }

        public enum MenuChoiceOperation
        {
            Insert = 1, Update = 2, Delete = 3 , Display = 4
        }

        public enum OperationOnParticularData
        {
            Yes = 1, No = 2
        }
    }
}
