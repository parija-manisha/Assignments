using StudentDetailsMultipleLayers.DataAccess;
using StudentDetailsMultipleLayers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Business
{
    public class StudentClassLogic
    {
        public static bool AssignStudentToClass(int studentId, int classId)
        {
            return StudentClassOperation.AssignStudentToClass(studentId, classId);
        }

        public List<StudentClassInfo> DisplayStudentClass()
        {
            return StudentClassOperation.StudentClassDisplay();
        }

    }
}
