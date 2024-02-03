using StudentDetailsMultipleLayers.DataAccess;
using StudentDetailsMultipleLayers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Business
{
    public class StudentLogic
    {
        public static bool AddStudent(StudentDTO student)
        {
            Student entity = ConvertToEntity(student);
            return StudentOperation.StudentInsert(entity, typeof(Student));
        }

        public static bool ModifyStudent(int studentID, string col, string data)
        {
            return StudentOperation.StudentUpdate(studentID, col, data);
        }

        public static bool RemoveStudent(int studentID)
        {
            return StudentOperation.StudentDelete(studentID);
        }

        public static List<StudentDTO> DisplayStudent()
        {
            return StudentOperation.DisplayStudent();
        }

        private static Student ConvertToEntity(StudentDTO studentDTO)
        {
            Student student = new Student();

            var propertiesDTO = typeof(StudentDTO).GetProperties();
            var properties = typeof(Student).GetProperties();

            foreach (var propertyDTO in propertiesDTO)
            {
                var matchingPropertyEntity = properties.FirstOrDefault(p => p.Name == propertyDTO.Name);

                if (matchingPropertyEntity != null)
                {
                    var valueDTO = propertyDTO.GetValue(studentDTO);
                    matchingPropertyEntity.SetValue(student, valueDTO);
                }
            }

            return student;
        }
    }
}
