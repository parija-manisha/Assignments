using StudentDetailsMultipleLayers.Model;
using StudentDetailsMultipleLayers.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.DataAccess
{
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
                Logger.AddError("\nInsertion Failed\n", ex);
                return false;
            }
        }

        public static bool CourseUpdate(int courseID, string col, string data)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    CourseDetail existingCourse = context.CourseDetails.Find(courseID);

                    if (existingCourse != null)
                    {
                        var property = typeof(CourseDetail).GetProperty(col);
                        var convertedData = Convert.ChangeType(data, property.PropertyType);
                        property.SetValue(existingCourse, convertedData);
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

        public static bool CourseDelete(int courseID)
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
                Logger.AddError("\nDeletion Failed\n", ex);
                return false;
            }
        }

        public static bool AssignCourseToClass(int courseID, int classID)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    CourseDetail course = context.CourseDetails.Find(courseID);
                    ClassDetail newClass = context.ClassDetails.Find(classID);

                    if (course != null && newClass != null)
                    {
                        CourseClass courseClass = new CourseClass
                        {
                            CourseID = courseID,
                            ClassID = classID,
                        };

                        context.CourseClasses.Add(courseClass);
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

        public static List<CourseDetailDTO> DisplayCourse()
        {
            List<CourseDetailDTO> allCourseDTO = null;
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    List<CourseDetail> allCourses = context.CourseDetails.ToList();

                    allCourseDTO = new List<CourseDetailDTO>();
                    foreach (var course in allCourses)
                    {
                        CourseDetailDTO courseDTO = new CourseDetailDTO
                        {
                            CourseID = course.CourseID,
                            CourseName = course.CourseName,
                        };
                        allCourseDTO.Add(courseDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nError in Displaying\n", ex);
            }
            return allCourseDTO;
        }

        public static CourseDetail GetCourseById(int id)
        {
            using (var context = new StudentDetailsEntities())
            {
                return context.CourseDetails.Find(id);
            }
        }
    }
}
