using ProjectStudentDetailsMultipleLayer.Models;
using ProjectStudentDetailsMultipleLayer.Util;
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Runtime.Remoting.Contexts;

namespace ProjectStudentDetailsMultipleLayer.DataAccess
{
    public class StudentOperation
    {
        public static bool StudentInsert(Student student, Type entityType)
        {
            try
            {
                DBContextValue.GetContext().Set(entityType).Add(student);
                int affectedRows = DBContextValue.GetContext().SaveChanges();

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
                return false;
            }
        }

        public static bool StudentUpdate(int studentID, string col, string data)
        {
            try
            {

                Student existingStudent = DBContextValue.GetContext().Students.Find(studentID);

                if (existingStudent != null)
                {
                    var property = typeof(Student).GetProperty(col);
                    var convertedData = Convert.ChangeType(data, property.PropertyType);
                    property.SetValue(existingStudent, convertedData);
                    int affectedRows = DBContextValue.GetContext().SaveChanges();

                    return affectedRows > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed", ex);
                return false;
            }
        }

        public static bool StudentDelete(Student studentID)
        {
            try
            {
                    Student existingStudent = DBContextValue.GetContext().Students.Find(studentID);

                    if (existingStudent != null)
                    {
                        DBContextValue.GetContext().Students.Remove(existingStudent);
                        int affectedRows = DBContextValue.GetContext().SaveChanges();

                        return affectedRows > 0;
                    }
                    else
                    {
                        return false;
                    }
            }
            catch (Exception ex)
            {
                Logger.AddError("Deletion Failed", ex);
                return false;
            }
        }

        public static Student GetStudentById(int id)
        {
            return DBContextValue.GetContext().Students.Find(id);
        }
    }

    public class CourseOperation
    {
        public static bool CourseInsert(Course course, Type entityType)
        {
            try
            {
                DBContextValue.GetContext().Set(entityType).Add(course);
                int affectedRows = DBContextValue.GetContext().SaveChanges();

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
                return false;
            }
        }

        public static bool CourseUpdate(int courseID, string col, string data)
        {
            try
            {
                if (courseID > 0)
                {
                    Course existingcourse = DBContextValue.GetContext().Courses.Find(courseID);

                    if (existingcourse != null)
                    {
                        var property = typeof(Course).GetProperty(col);
                        var convertedData = Convert.ChangeType(data, property.PropertyType);
                        property.SetValue(existingcourse, convertedData);
                        int affectedRows = DBContextValue.GetContext().SaveChanges();

                        return affectedRows > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed", ex);
                return false;
            }
        }

        public static bool CourseDelete(Course courseID)
        {
            try
            {
                Course existingCourse = DBContextValue.GetContext().Courses.Find(courseID);

                if (existingCourse != null)
                {
                    DBContextValue.GetContext().Courses.Remove(existingCourse);
                    int affectedRows = DBContextValue.GetContext().SaveChanges();

                    return affectedRows > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Deletion Failed", ex);
                return false;
            }
        }

        public static Course GetCourseById(int id)
        {
            return DBContextValue.GetContext().Courses.Find(id);
        }
    }

    public class ClassDetailOperation
    {
        public static bool ClassInsert(Class_Detail classDetail, Type entityType)
        {
            try
            {
                DBContextValue.GetContext().Set(entityType).Add(classDetail);
                int affectedRows = DBContextValue.GetContext().SaveChanges();

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed", ex);
                return false;
            }
        }

        public static bool ClassUpdate(int classID, string col, string data)
        {
            try
            {
                if (classID > 0)
                {
                    Class_Detail existingclass = DBContextValue.GetContext().Class_Detail.Find(classID);

                    if (existingclass != null)
                    {
                        var property = typeof(Class_Detail).GetProperty(col);
                        var convertedData = Convert.ChangeType(data, property.PropertyType);
                        property.SetValue(existingclass, convertedData);
                        int affectedRows = DBContextValue.GetContext().SaveChanges();

                        return affectedRows > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed", ex);
                return false;
            }
        }

        public static bool ClassDelete(Class_Detail classID)
        {
            try
            {
                Class_Detail existingclass = DBContextValue.GetContext().Class_Detail.Find(classID);

                if (existingclass != null)
                {
                    DBContextValue.GetContext().Class_Detail.Remove(existingclass);
                    int affectedRows = DBContextValue.GetContext().SaveChanges();

                    return affectedRows > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Deletion Failed", ex);
                return false;
            }
        }

        public static Class_Detail GetClassById(int id)
        {
            return DBContextValue.GetContext().Class_Detail.Find(id);
        }
    }
}
