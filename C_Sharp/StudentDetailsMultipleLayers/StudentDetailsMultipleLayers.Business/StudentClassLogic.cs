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
        public static bool AssignStudentToClass(StudentClassDTO studentClassDTO)
        {
            StudentClass studentClassEntity = ConvertToEntity(studentClassDTO);
            return StudentClassOperation.AssignStudentToClass(studentClassEntity, typeof(StudentClass));
        }

        public List<StudentClassInfo> DisplayStudentClass()
        {
            return StudentClassOperation.StudentClassDisplay();
        }

        private static StudentClass ConvertToEntity(StudentClassDTO studentClassDTO)
        {
            StudentClass studentClass = new StudentClass();
            var propertiesDTO = typeof(StudentClassDTO).GetProperties();
            var properties = typeof(StudentClass).GetProperties();

            foreach (var propertyDTO in propertiesDTO )
            {
                var matchingPropertyEntity = properties.FirstOrDefault(p => p.Name == propertyDTO.Name);

                if (matchingPropertyEntity != null)
                {
                    var valueDTO = propertyDTO.GetValue(studentClassDTO);
                    matchingPropertyEntity.SetValue(studentClass, valueDTO);
                }
            }
            return studentClass;
        }

    }
}
