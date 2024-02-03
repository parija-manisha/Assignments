using ProjectStudentDetailsMultipleLayer.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static ProjectStudentDetailsMultipleLayer.BusinessLogic.BusinessLogic;
using ProjectStudentDetailsMultipleLayer.Models;

namespace ProjectStudentDetailsMultipleLayer
{
    internal class Program
    {
        static void Main()
        {
            bool exit = false;
            do
            {
                Console.WriteLine("Enter your table choice(1 for Student, 2 for Course, 3 for Class, 4 for StudentClass, 5 for CourseClass, 6 for Exit");
                if (Utility.TryParseEnum(Console.ReadLine(), out EnumValues.MenuChoiceTable choiceTable))
                {
                    if (choiceTable == EnumValues.MenuChoiceTable.Exit)
                    {
                        break;
                    }
                    Console.WriteLine("Enter your operation choice (1 for Insert, 2 for Update, 3 for Delete, 4 for Display):");
                    if (Utility.TryParseEnum(Console.ReadLine(), out EnumValues.MenuChoice choice))
                    {
                        switch (choiceTable)
                        {
                            case EnumValues.MenuChoiceTable.Student:
                                switch (choice)
                                {
                                    case EnumValues.MenuChoice.Insert:
                                        StudentInsertData(typeof(StudentDTO));
                                        break;
                                    case EnumValues.MenuChoice.Update:
                                        StudentUpdateData();
                                        break;
                                    case EnumValues.MenuChoice.Delete:
                                        StudentDeleteData();
                                        break;
                                    case EnumValues.MenuChoice.Display:
                                        //StudentDisplayData();
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Operation Choice");
                                        break;
                                }
                                break;
                            case EnumValues.MenuChoiceTable.Course:
                                switch (choice)
                                {
                                    case EnumValues.MenuChoice.Insert:
                                        CourseInsertData(typeof(CourseDTO));
                                        break;
                                    case EnumValues.MenuChoice.Update:
                                        CourseUpdateData();
                                        break;
                                    case EnumValues.MenuChoice.Delete:
                                        CourseDeleteData();
                                        break;
                                    case EnumValues.MenuChoice.Display:
                                        //CourseDisplayData();
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Operation Choice");
                                        break;
                                }
                                break;
                            case EnumValues.MenuChoiceTable.Class:
                                switch (choice)
                                {
                                    case EnumValues.MenuChoice.Insert:
                                        ClassInsertData(typeof(ClassDetailDTO));
                                        break;
                                    case EnumValues.MenuChoice.Update:
                                        ClassUpdateData();
                                        break;
                                    case EnumValues.MenuChoice.Delete:
                                        ClassDeleteData();
                                        break;
                                    case EnumValues.MenuChoice.Display:
                                        //ClassDetailLogic.ClassDisplayData();
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Operation Choice");
                                        break;
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid Table Choice");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Operation Choice");
                    }
                }
            } while (!exit);
        }

        static void StudentInsertData(Type entityType)
        {
            try
            {
                var entity = Activator.CreateInstance(entityType);
                var properties = entityType.GetProperties();
                foreach (var property in properties)
                {
                    if(property.Name == "StudentID")
                    {
                        continue;
                    }
                    Console.WriteLine($"Enter value for {property.Name}:");

                    string userInput = Console.ReadLine();

                    if (property.PropertyType == typeof(int))
                    {
                        if (!Utility.TryParseInt(userInput, out int intValue))
                        {
                            Console.WriteLine($"Invalid {property.Name} format. Please enter a valid integer.");
                            break;
                        }
                        Utility.SetValue(entity, intValue.ToString(), property);
                    }
                    else
                    {
                        if (Utility.IsNullOrEmpty(userInput))
                        {
                            Console.WriteLine($"{property.Name} cannot be empty.");
                            break;
                        }
                        Utility.SetValue(entity, userInput, property);
                    }
                }
                bool addSuccess = ClassDetailLogic.AddStudent((StudentDTO)entity);

                if (addSuccess)
                {
                    Console.WriteLine("Student added successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed. Error:", ex);
            }
        }

        static void CourseInsertData(Type entityType)
        {
            try
            {
                var entity = Activator.CreateInstance(entityType);
                var properties = entityType.GetProperties();
                foreach (var property in properties)
                {
                    if (property.Name == "CourseID")
                    {
                        continue;
                    }
                    Console.WriteLine($"Enter value for {property.Name}:");

                    string userInput = Console.ReadLine();

                    if (property.PropertyType == typeof(int))
                    {
                        if (!Utility.TryParseInt(userInput, out int intValue))
                        {
                            Console.WriteLine($"Invalid {property.Name} format. Please enter a valid integer.");
                            break;
                        }
                        Utility.SetValue(entity, intValue.ToString(), property);
                    }
                    else
                    {
                        if (Utility.IsNullOrEmpty(userInput))
                        {
                            Console.WriteLine($"{property.Name} cannot be empty.");
                            break;
                        }
                        Utility.SetValue(entity, userInput, property);
                    }
                }
                bool addSuccess = CourseLogic.AddCourse((CourseDTO)entity);

                if (addSuccess)
                {
                    Console.WriteLine("Course added successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed. Error:", ex);
            }
        }
        
        static void ClassInsertData(Type entityType)
        {
            try
            {
                var entity = Activator.CreateInstance(entityType);
                var properties = entityType.GetProperties();
                foreach (var property in properties)
                {
                    if (property.Name == "ClassID")
                    {
                        continue;
                    }
                    Console.WriteLine($"Enter value for {property.Name}:");

                    string userInput = Console.ReadLine();

                    if (property.PropertyType == typeof(int))
                    {
                        if (!Utility.TryParseInt(userInput, out int intValue))
                        {
                            Console.WriteLine($"Invalid {property.Name} format. Please enter a valid integer.");
                            break;
                        }
                        Utility.SetValue(entity, intValue.ToString(), property);
                    }
                    else
                    {
                        if (Utility.IsNullOrEmpty(userInput))
                        {
                            Console.WriteLine($"{property.Name} cannot be empty.");
                            break;
                        }
                        Utility.SetValue(entity, userInput, property);
                    }
                }
                bool addSuccess = ClassLogic.AddClass((ClassDetailDTO)entity);

                if (addSuccess)
                {
                    Console.WriteLine("Course added successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Insertion Failed.\n Error:", ex);
            }
        }

        static void StudentUpdateData()
        {
            try
            {
                Console.WriteLine("Enter the Student ID you want to update");
                int studentID = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Enter the Column to be Updated");
                string col = Console.ReadLine();

                Console.WriteLine("Enter the data to be updated");
                string data = Console.ReadLine();

                bool addSuccess = ClassDetailLogic.UpdateStudent(studentID, col, data);

                if (addSuccess)
                {
                    Console.WriteLine($"{col} updated successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed. \n Error:", ex);
            }
        }
        
        static void CourseUpdateData()
        {
            try
            {
                Console.WriteLine("Enter the Course ID you want to update");
                int columnID = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Enter the Column to be Updated");
                string col = Console.ReadLine();

                Console.WriteLine("Enter the data to be updated");
                string data = Console.ReadLine();

                bool addSuccess = CourseLogic.UpdateCourse(columnID, col, data);

                if (addSuccess)
                {
                    Console.WriteLine($"{col} updated successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed. \n Error:", ex);
            }
        }       
        
        static void ClassUpdateData()
        {
            try
            {
                Console.WriteLine("Enter the Class ID you want to update");
                int classID = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Enter the Column to be Updated");
                string col = Console.ReadLine();

                Console.WriteLine("Enter the data to be updated");
                string data = Console.ReadLine();

                bool addSuccess = ClassLogic.UpdateClass(classID, col, data);

                if (addSuccess)
                {
                    Console.WriteLine($"{col} updated successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed. \n Error:", ex);
            }
        }

        static void StudentDeleteData()
        {
            try
            {
                Console.WriteLine("Enter the Student ID you want to delete");
                int studentID = Convert.ToInt32(Console.ReadLine().Trim());

                bool addSuccess = ClassDetailLogic.DeleteStudent(studentID);

                if (addSuccess)
                {
                    Console.WriteLine($"Student id {studentID} deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed. \n Error:", ex);
            }
        }

        static void CourseDeleteData()
        {
            try
            {
                Console.WriteLine("Enter the Course ID you want to update");
                int columnID = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Enter the Column to be Updated");
                string col = Console.ReadLine();

                Console.WriteLine("Enter the data to be updated");
                string data = Console.ReadLine();

                bool addSuccess = CourseLogic.UpdateCourse(columnID, col, data);

                if (addSuccess)
                {
                    Console.WriteLine($"{col} updated successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed. \n Error:", ex);
            }
        }

        static void ClassDeleteData()
        {
            try
            {
                Console.WriteLine("Enter the Class ID you want to update");
                int classID = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Enter the Column to be Updated");
                string col = Console.ReadLine();

                Console.WriteLine("Enter the data to be updated");
                string data = Console.ReadLine();

                bool addSuccess = ClassLogic.UpdateClass(classID, col, data);

                if (addSuccess)
                {
                    Console.WriteLine($"{col} updated successfully!");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed. \n Error:", ex);
            }
        }

    }
}
