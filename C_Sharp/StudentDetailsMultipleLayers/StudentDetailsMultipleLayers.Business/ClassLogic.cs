using StudentDetailsMultipleLayers.DataAccess;
using StudentDetailsMultipleLayers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Business
{
    public class ClassLogic
    {
        public static bool AddClass(ClassDetailDTO newClass)
        {
            ClassDetail entity = ConvertToEntity(newClass);
            return ClassOperation.ClassInsert(entity, typeof(ClassDetail));
        }

        public static bool ModifyClass(int classID, string col, string data)
        {
            return ClassOperation.ClassUpdate(classID, col, data);
        }

        public static bool RemoveClass(int classID)
        {
            return ClassOperation.ClassDelete(classID);
        }

        public static List<ClassDetailDTO> DisplayClass()
        {
            return ClassOperation.DisplayClass();
        }

        private static ClassDetail ConvertToEntity(ClassDetailDTO classDetailDTO)
        {
            ClassDetail student = new ClassDetail();

            student.ClassID = classDetailDTO.ClassID; 


            var propertiesDTO = typeof(ClassDetailDTO).GetProperties();
            var properties = typeof(ClassDetail).GetProperties();

            foreach (var propertyDTO in propertiesDTO)
            {
                var matchingPropertyEntity = properties.FirstOrDefault(p => p.Name == propertyDTO.Name);

                if (matchingPropertyEntity != null)
                {
                    var valueDTO = propertyDTO.GetValue(classDetailDTO);
                    matchingPropertyEntity.SetValue(student, valueDTO);
                }
            }

            return student;
        }
    }
}
