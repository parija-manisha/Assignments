using StudentDetailsMultipleLayers.Model;
using StudentDetailsMultipleLayers.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.DataAccess
{
    public class ClassOperation
    {
        public static bool ClassInsert(ClassDetail newClass, Type entityType)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    context.Set(entityType).Add(newClass);
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

        public static bool ClassUpdate(int classID, string col, string data)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    ClassDetail existingClass = context.ClassDetails.Find(classID);

                    if (existingClass != null)
                    {
                        var property = typeof(ClassDetail).GetProperty(col);
                        var convertedData = Convert.ChangeType(data, property.PropertyType);
                        property.SetValue(existingClass, convertedData);
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

        public static bool ClassDelete(int classID)
        {
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    ClassDetail existingClass = context.ClassDetails.Find(classID);

                    if (existingClass != null)
                    {
                        context.ClassDetails.Remove(existingClass);
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

        public static List<ClassDetailDTO> DisplayClass()
        {
            List<ClassDetailDTO> allClassDTO = null;
            try
            {
                using (var context = new StudentDetailsEntities())
                {
                    List<ClassDetail> allClasses = context.ClassDetails.ToList();

                    allClassDTO = new List<ClassDetailDTO>();
                    foreach (var newclass in allClasses)
                    {
                        ClassDetailDTO classDTO = new ClassDetailDTO
                        {
                            ClassID = newclass.ClassID,
                            ClassName = newclass.ClassName,
                        };
                        allClassDTO.Add(classDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nError in Displaying\n", ex);
            }
            return allClassDTO;
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
