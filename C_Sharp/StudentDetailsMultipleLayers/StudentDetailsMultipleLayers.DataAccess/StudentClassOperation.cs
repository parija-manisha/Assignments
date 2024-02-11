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
        public static bool AssignStudentToClass(StudentClass studentClass, Type entityType)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    context.Set(entityType).Add(studentClass);
                    int affectedRows = context.SaveChanges();
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
            }

            return false;
        }

        public static List<StudentClassInfo> StudentClassDisplay()
        {
            List<StudentClassInfo> students = new List<StudentClassInfo>();

            using (var connection = Connection.ConnectADO())
            {
                connection.Open();

                string query = "SELECT StudentClass.StudentID, StudentClass.ClassID, ClassDetail.ClassName " +
                               "FROM StudentClass " +
                               "JOIN ClassDetail ON StudentClass.ClassID = ClassDetail.ClassID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentClassInfo student = new StudentClassInfo
                            {
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                ClassID= Convert.ToInt32(reader["ClassID"]),
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
