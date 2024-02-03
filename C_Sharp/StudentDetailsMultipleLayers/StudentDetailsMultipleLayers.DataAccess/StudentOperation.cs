using StudentDetailsMultipleLayers.Model;
using StudentDetailsMultipleLayers.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.DataAccess
{
    public class StudentOperation
    {
        public static bool StudentInsert(Student student, Type entityType)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    context.Set(entityType).Add(student);
                    int affectedRows = context.SaveChanges();
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nInsertion Failed\n", ex);
                return false;
            }
        }

        public static bool StudentUpdate(int studentID, string col, string data)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    Student existingStudent = context.Students.Find(studentID);

                    if (existingStudent != null)
                    {
                        var property = typeof(Student).GetProperty(col);
                        var convertedData = Convert.ChangeType(data, property.PropertyType);
                        property.SetValue(existingStudent, convertedData);
                        int affectedRows = context.SaveChanges();

                        return affectedRows > 0;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.AddError("\nUpdation Failed\n", ex);
                return false;
            }
        }

        public static bool StudentDelete(int studentID)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    Student existingStudent = context.Students.Find(studentID);

                    if (existingStudent != null)
                    {
                        context.Students.Remove(existingStudent);
                        int affectedRows = context.SaveChanges();

                        return affectedRows > 0;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nDeletion Failed\n", ex);
                return false;
            }
        }

        public static List<StudentDTO> DisplayStudent()
        {
            List<StudentDTO> allStudentDTO = null;
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    List<Student> allStudents = context.Students.ToList();

                    allStudentDTO = new List<StudentDTO>();
                    foreach (var student in allStudents)
                    {
                        StudentDTO studentDTO = new StudentDTO
                        {
                            StudentID = student.StudentID,
                            StudentName = student.StudentName,
                            Gender = student.Gender,
                            PhoneNumber = student.PhoneNumber,
                            DateOfBirth = student.DateOfBirth,
                            Email = student.Email
                        };
                        allStudentDTO.Add(studentDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nError in Displaying\n", ex);
            }
            return allStudentDTO;
        }

        public static Student GetStudentById(int id)
        {
            using (var context = new StudentDetailsEntities())
            {
                return context.Students.Find(id);
            }
        }
    }
}
