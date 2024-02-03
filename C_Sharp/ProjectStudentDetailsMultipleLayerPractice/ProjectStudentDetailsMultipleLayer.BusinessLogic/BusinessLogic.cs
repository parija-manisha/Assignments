using ProjectStudentDetailsMultipleLayer.DataAccess;
using ProjectStudentDetailsMultipleLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStudentDetailsMultipleLayer.BusinessLogic
{
    public class BusinessLogic
    {
        public class StudentLogic
        {
            public static bool AddStudent(StudentDTO student)
            {
                Student entityStudent = ConvertToEntity(student);
                return StudentOperation.StudentInsert(entityStudent, typeof(Student));
            }

            public static bool UpdateStudent(int studentID, string col, string data) {
                return StudentOperation.StudentUpdate(studentID, col, data);
            }

            public static bool DeleteStudent(int studentID)
            {
                Student studentDetailID = StudentOperation.GetStudentById(studentID);
                return StudentOperation.StudentDelete(studentDetailID);
            }
        }
        public class CourseLogic
        {
            public static bool AddCourse(CourseDTO course)
            {
                Course entityCourse = ConvertToEntity(course);
                return CourseOperation.CourseInsert(entityCourse, typeof(Course));
            }

            public static bool UpdateCourse(int courseID, string col, string data)
            {
                return CourseOperation.CourseUpdate(courseID, col, data);
            }

            public static bool DeleteCourse(int courseID)
            {
                Course courseDetailID = CourseOperation.GetCourseById(courseID);
                return CourseOperation.CourseDelete(courseDetailID);
            }
        }
        public class ClassDetailLogic
        {
            public static bool AddClass(ClassDetailDTO newclass)
            {
                Class_Detail entityClass = ConvertToEntity(newclass);
                return ClassDetailOperation.ClassInsert(entityClass, typeof(Class_Detail));
            }

            public static bool UpdateClass(int classID, string col, string data)
            {
                return ClassDetailOperation.ClassUpdate(classID, col, data);
            }

            public static bool DeleteClass(int classID)
            {
                Class_Detail classdetailID = ClassDetailOperation.GetClassById(classID);
                return ClassDetailOperation.ClassDelete(classdetailID);
            }

            //public static bool ClassDisplayData()
            //{

            //}
        }



        private static Student ConvertToEntity(StudentDTO studentDTO)
        {
            Student studentEntity = new Student();

            var propertiesDTO = typeof(StudentDTO).GetProperties();
            var propertiesEntity = typeof(Student).GetProperties();

            foreach (var propertyDTO in propertiesDTO)
            {
                var matchingPropertyEntity = propertiesEntity.FirstOrDefault(p => p.Name == propertyDTO.Name);

                if (matchingPropertyEntity != null)
                {
                    var valueDTO = propertyDTO.GetValue(studentDTO);
                    matchingPropertyEntity.SetValue(studentEntity, valueDTO);
                }
            }

            return studentEntity;
        }

        private static Course ConvertToEntity(CourseDTO courseDTO)
        {
            Course courseEntity = new Course();

            var propertiesDTO = typeof(CourseDTO).GetProperties();
            var propertiesEntity = typeof(Course).GetProperties();

            foreach (var propertyDTO in propertiesDTO)
            {
                var matchingPropertyEntity = propertiesEntity.FirstOrDefault(p => p.Name == propertyDTO.Name);

                if (matchingPropertyEntity != null)
                {
                    var valueDTO = propertyDTO.GetValue(courseDTO);
                    matchingPropertyEntity.SetValue(courseEntity, valueDTO);
                }
            }

            return courseEntity;
        }

        private static Class_Detail ConvertToEntity(ClassDetailDTO classDetailDTO)
        {
            Class_Detail classDetailEntity = new Class_Detail();

            var propertiesDTO = typeof(ClassDetailDTO).GetProperties();
            var propertiesEntity = typeof(Class_Detail).GetProperties();

            foreach (var propertyDTO in propertiesDTO)
            {
                var matchingPropertyEntity = propertiesEntity.FirstOrDefault(p => p.Name == propertyDTO.Name);

                if (matchingPropertyEntity != null)
                {
                    var valueDTO = propertyDTO.GetValue(classDetailDTO);
                    matchingPropertyEntity.SetValue(classDetailEntity, valueDTO);
                }
            }

            return classDetailEntity;
        }

    }
}
