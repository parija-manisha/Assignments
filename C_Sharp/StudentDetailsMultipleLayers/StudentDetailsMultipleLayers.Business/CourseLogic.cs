using StudentDetailsMultipleLayers.DataAccess;
using StudentDetailsMultipleLayers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Business
{
    public class CourseLogic
    {
        public static bool AddCourse(CourseDetailDTO course)
        {
            CourseDetail entity = ConvertToEntity(course);
            return CourseOperation.CourseInsert(entity, typeof(CourseDetail));
        }

        public static bool ModifyCourse(int courseID, string col, string data)
        {
            return CourseOperation.CourseUpdate(courseID, col, data);
        }

        public static bool RemoveCourse(int courseID)
        {
            return CourseOperation.CourseDelete(courseID);
        }

        public static bool AssignCourseToClass(int courseId, int classId)
        {
            return CourseOperation.AssignCourseToClass(courseId, classId);
        }

        public static List<CourseDetailDTO> DisplayCourse()
        {
            return CourseOperation.DisplayCourse();
        }

        private static CourseDetail ConvertToEntity(CourseDetailDTO courseDetailDTO)
        {
            CourseDetail student = new CourseDetail();

            var propertiesDTO = typeof(CourseDetailDTO).GetProperties();
            var properties = typeof(CourseDetail).GetProperties();

            foreach (var propertyDTO in propertiesDTO)
            {
                var matchingPropertyEntity = properties.FirstOrDefault(p => p.Name == propertyDTO.Name);

                if (matchingPropertyEntity != null)
                {
                    var valueDTO = propertyDTO.GetValue(courseDetailDTO);
                    matchingPropertyEntity.SetValue(student, valueDTO);
                }
            }

            return student;
        }
    }
}
