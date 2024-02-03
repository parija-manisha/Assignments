using StudentDetailsLayers.DataAccess;
using StudentDetailsLayers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentDetailsLayers.DataAccess.DataAccess;

namespace StudentDetailsLayers.Business
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

            public static bool UpdateStudent(int studentID, string col, string data)
            {
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
            public static bool AddCourse(CourseDetailDTO course)
            {
                CourseDetail entityCourse = ConvertToEntity(course);
                return CourseOperation.CourseInsert(entityCourse, typeof(CourseDetail));
            }

            public static bool UpdateCourse(int courseID, string col, string data)
            {
                return CourseOperation.CourseUpdate(courseID, col, data);
            }

            public static bool DeleteCourse(int courseID)
            {
                CourseDetail courseDetailID = CourseOperation.GetCourseById(courseID);
                return CourseOperation.CourseDelete(courseDetailID);
            }
        }
       
        public class ClassDetailLogic
        {
            public static bool AddClass(ClassDetailDTO newclass)
            {
                ClassDetail entityClass = ConvertToEntity(newclass);
                return ClassDetailOperation.ClassInsert(entityClass, typeof(ClassDetail));
            }

            public static bool UpdateClass(int classID, string col, string data)
            {
                return ClassDetailOperation.ClassUpdate(classID, col, data);
            }

            public static bool DeleteClass(int classID)
            {
                ClassDetail classdetailID = ClassDetailOperation.GetClassById(classID);
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

        private static CourseDetail ConvertToEntity(CourseDetailDTO courseDTO)
        {
            CourseDetail courseEntity = new CourseDetail();

            var propertiesDTO = typeof(CourseDetailDTO).GetProperties();
            var propertiesEntity = typeof(CourseDetail).GetProperties();

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

        private static ClassDetail ConvertToEntity(ClassDetailDTO classDetailDTO)
        {
            ClassDetail classDetailEntity = new ClassDetail();

            var propertiesDTO = typeof(ClassDetailDTO).GetProperties();
            var propertiesEntity = typeof(ClassDetail).GetProperties();

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
