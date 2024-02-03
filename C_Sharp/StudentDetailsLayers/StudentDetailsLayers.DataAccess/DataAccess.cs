using StudentDetailsLayers.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsLayers.DataAccess
{
    public class DataAccess
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
                    Logger.AddError("Insertion Failed", ex);
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
                    Logger.AddError("Updation Failed", ex);
                    return false;
                }
            }

            public static bool StudentDelete(Student studentID)
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
                    Logger.AddError("Deletion Failed", ex);
                    return false;
                }
            }

            public static Student GetStudentById(int id)
            {
                using (var context = new StudentDetailsEntities())
                {
                    return context.Students.Find(id);
                }
            }
        }

        public class CourseOperation
        {
            public static bool CourseInsert(CourseDetail course, Type entityType)
            {
                try
                {
                    using (var context = new StudentDetailsEntities())
                    {
                        context.Set(entityType).Add(course);
                        int affectedRows = context.SaveChanges();

                        return affectedRows > 0;
                    }
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
                    using (var context = new StudentDetailsEntities())
                    {
                        if (courseID > 0)
                        {
                            CourseDetail existingcourse = context.CourseDetails.Find(courseID);

                            if (existingcourse != null)
                            {
                                var property = typeof(CourseDetail).GetProperty(col);
                                var convertedData = Convert.ChangeType(data, property.PropertyType);
                                property.SetValue(existingcourse, convertedData);
                                int affectedRows = context.SaveChanges();

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
                }
                catch (Exception ex)
                {
                    Logger.AddError("Updation Failed", ex);
                    return false;
                }
            }

            public static bool CourseDelete(CourseDetail courseID)
            {
                try
                {
                    using (var context = new StudentDetailsEntities())
                    {
                        CourseDetail existingCourse = context.CourseDetails.Find(courseID);

                        if (existingCourse != null)
                        {
                            context.CourseDetails.Remove(existingCourse);
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
                    Logger.AddError("Deletion Failed", ex);
                    return false;
                }
            }

            public static CourseDetail GetCourseById(int id)
            {
                using (var context = new StudentDetailsEntities())
                {
                    return context.CourseDetails.Find(id);
                }
            }
        }

        public class ClassDetailOperation
        {
            public static bool ClassInsert(ClassDetail classDetail, Type entityType)
            {
                try
                {
                    using (var context = new StudentDetailsEntities())
                    {
                        context.Set(entityType).Add(classDetail);
                        int affectedRows = context.SaveChanges();

                        return affectedRows > 0;
                    }
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
                    using (var context = new StudentDetailsEntities())
                    {
                        if (classID > 0)
                        {
                            ClassDetail existingclass = context.ClassDetails.Find(classID);

                            if (existingclass != null)
                            {
                                var property = typeof(ClassDetail).GetProperty(col);
                                var convertedData = Convert.ChangeType(data, property.PropertyType);
                                property.SetValue(existingclass, convertedData);
                                int affectedRows = context.SaveChanges();

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
                }
                catch (Exception ex)
                {
                    Logger.AddError("Updation Failed", ex);
                    return false;
                }
            }

            public static bool ClassDelete(ClassDetail classID)
            {
                try
                {
                    using (var context = new StudentDetailsEntities())
                    {
                        ClassDetail existingclass = context.ClassDetails.Find(classID);

                        if (existingclass != null)
                        {
                            context.ClassDetails.Remove(existingclass);
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
                    Logger.AddError("Deletion Failed", ex);
                    return false;
                }
            }

            public static ClassDetail GetClassById(int id)
            {
                using (var context = new StudentDetailsEntities())
                {
                    return context.ClassDetails.Find(id);
                }
            }
        }
    }
}
