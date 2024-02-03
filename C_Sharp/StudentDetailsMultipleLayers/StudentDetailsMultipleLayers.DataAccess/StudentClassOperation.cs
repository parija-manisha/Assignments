using StudentDetailsMultipleLayers.Model;
using StudentDetailsMultipleLayers.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.DataAccess
{
    public class StudentClassOperation
    {
        public static bool AssignStudentToClass(int studentID, int classID)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    Student student = context.Students.Find(studentID);
                    ClassDetail newClass = context.ClassDetails.Find(classID);

                    if (student != null && newClass != null)
                    {
                        StudentClass studentClass = new StudentClass
                        {
                            StudentID = studentID,
                            ClassID = classID,
                        };

                        context.StudentClasses.Add(studentClass);
                        int affectedRows = context.SaveChanges();
                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
            }

            return false;
        }

        // Inside your StudentDataAccess class

        public static List<StudentClassInfo> StudentClassDisplay()
        {
            List<StudentClassInfo> students = new List<StudentClassInfo>();

            using (var connection = Connection.Connect())
            {
                connection.Open();

                string query = "SELECT Students.StudentID, Students.StudentName, Class.ClassName " +
                               "FROM Students " +
                               "JOIN Class ON Students.ClassID = Class.ClassID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentClassInfo student = new StudentClassInfo
                            {
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                StudentName = reader["StudentName"].ToString(),
                                ClassName = reader["ClassName"].ToString(),
                            };

                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }

        public static StudentClass GetClassById(int id)
        {
            using (var context = new StudentDetailsEntities())
            {
                return context.StudentClasses.Find(id);
            }
        }
    }
}
