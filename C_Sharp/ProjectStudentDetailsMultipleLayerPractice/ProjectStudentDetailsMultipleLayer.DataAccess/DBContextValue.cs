using ProjectStudentDetailsMultipleLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStudentDetailsMultipleLayer.Util
{
    internal class DBContextValue
    {
        private static ENTITY_FRAMEWORK_ASSIGNMENTEntities1 context;

        public static ENTITY_FRAMEWORK_ASSIGNMENTEntities1 GetContext()
        {
            if (context == null)
            {
                context = new ENTITY_FRAMEWORK_ASSIGNMENTEntities1();
            }

            return context;
        }
    }
}
