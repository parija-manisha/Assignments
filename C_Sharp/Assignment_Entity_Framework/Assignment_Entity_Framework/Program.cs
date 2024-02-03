using System;
using System.Runtime.Remoting.Services;
using System.Xml.Linq;

namespace Assignment_Entity_Framework
{
    internal class Program
    {
        private static ENTITY_FRAMEWORK_ASSIGNMENTEntities3 context
                              = new ENTITY_FRAMEWORK_ASSIGNMENTEntities3();
        enum MenuChoice
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        enum MenuChoiceTable
        {
            Student = 1,
            Course = 2,
            Class = 3,
            Exit = 4
        }

        static void Main()
        {
            bool exit = false;
            using (context)
            {
                do
                {
                    Console.WriteLine("Enter your table choice (1 for Student, 2 for Course, 3 for Class, 4 for exit):");
                    if (Enum.TryParse(Console.ReadLine(), out MenuChoiceTable choiceTable))
                    {
                        if (choiceTable == MenuChoiceTable.Exit)
                        {
                            exit = true;
                            break;
                        }

                        Console.WriteLine("Enter your operation choice (1 for Insert, 2 for Update, 3 for Delete):");
                        if (Enum.TryParse(Console.ReadLine(), out MenuChoice choice))
                        {
                            switch (choiceTable)
                            {
                                case MenuChoiceTable.Student:
                                    switch (choice)
                                    {
                                        case MenuChoice.Insert:
                                            StudentInsert();
                                            break;
                                        case MenuChoice.Update:
                                            StudentUpdate();
                                            break;
                                        case MenuChoice.Delete:
                                            StudentDelete();
                                            break;
                                        default:
                                            Console.WriteLine("Invalid Operation Choice");
                                            break;
                                    }
                                    break;
                                case MenuChoiceTable.Course:
                                    switch (choice)
                                    {
                                        case MenuChoice.Insert:
                                            CourseInsert();
                                            break;
                                        case MenuChoice.Update:
                                            CourseUpdate();
                                            break;
                                        case MenuChoice.Delete:
                                            CourseDelete();
                                            break;
                                        default:
                                            Console.WriteLine("Invalid Operation Choice");
                                            break;
                                    }
                                    break;
                                case MenuChoiceTable.Class:
                                    switch (choice)
                                    {
                                        case MenuChoice.Insert:
                                            ClassInsert();
                                            break;
                                        case MenuChoice.Update:
                                            ClassUpdate();
                                            break;
                                        case MenuChoice.Delete:
                                            ClassDelete();
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
                    else
                    {
                        Console.WriteLine("Invalid Table Choice");
                    }
                } while (!exit);
            }
        }

        static void StudentInsert()
        {
            try
            {
                Console.WriteLine("Enter the Student ID:");
                if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
                {
                    Console.WriteLine("Invalid Student ID format. Please enter a valid positive integer.");
                    return;
                }

                Console.WriteLine("Enter the name:");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    return;
                }

                Console.WriteLine("Enter the gender:");
                string gender = Console.ReadLine();
                if (string.IsNullOrEmpty(gender))
                {
                    Console.WriteLine("Please enter your gender");
                    return;
                }

                Console.WriteLine("Enter the date of birth (yyyy-MM-dd):");
                string dobInput = Console.ReadLine();
                DateTime dob;
                if (!DateTime.TryParse(dobInput, out dob))
                {
                    Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                    return;
                }

                Console.WriteLine("Enter the phone number:");
                string phone = Console.ReadLine();
                int phone_number;
                if (string.IsNullOrEmpty(phone) && !int.TryParse(phone, out phone_number) && phone.Length <= 10)
                {
                    Console.WriteLine("Invalid Phone Number");
                }

                var student = new Student
                {
                    StudentID = studentId,
                    StudentName = name,
                    Gender = gender,
                    DateOfBirth = dob,
                    PhoneNumber = phone
                };

                context.Students.Add(student);
                context.SaveChanges();

                Console.WriteLine("Student added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void CourseInsert()
        {
            try
            {
                Console.WriteLine("Enter the Course ID:");
                int courseId;
                if (!int.TryParse(Console.ReadLine(), out courseId))
                {
                    Console.WriteLine("Invalid Course ID format. Please enter a valid integer.");
                    return;
                }

                Console.WriteLine("Enter the Course Name:");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    return;
                }

                var course = new Course
                {
                    CourseID = courseId,
                    CourseName = name
                };

                context.Courses.Add(course);
                context.SaveChanges();

                Console.WriteLine("Course added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ClassInsert()
        {
            try
            {
                Console.WriteLine("Enter the Class ID:");
                int classId;
                if (!int.TryParse(Console.ReadLine(), out classId))
                {
                    Console.WriteLine("Invalid Class ID format. Please enter a valid integer.");
                    return;
                }

                Console.WriteLine("Enter the Student ID:");
                int studentId;
                if (!int.TryParse(Console.ReadLine(), out studentId))
                {
                    Console.WriteLine("Invalid Student ID format. Please enter a valid integer.");
                    return;
                }

                Console.WriteLine("Enter the Course ID:");
                int courseId;
                if (!int.TryParse(Console.ReadLine(), out courseId))
                {
                    Console.WriteLine("Invalid Course ID format. Please enter a valid integer.");
                    return;
                }

                var newClass = new Class
                {
                    Class_ID = classId,
                    StudentID = studentId,
                    CourseID = courseId
                };

                context.Classes.Add(newClass);
                context.SaveChanges();

                Console.WriteLine("Class added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void StudentUpdate()
        {
            try
            {
                Console.WriteLine("Enter the Student ID:");
                int studentId;
                if (!int.TryParse(Console.ReadLine(), out studentId))
                {
                    Console.WriteLine("Invalid Student ID format. Please enter a valid integer.");
                    return;
                }

                var studentToBeUpdated = context.Students.Find(studentId);

                if (studentToBeUpdated == null)
                {
                    Console.WriteLine("Data not found");
                }
                else
                {
                    Console.WriteLine("Enter the Column to be updated");
                    var col = Console.ReadLine();
                    Console.WriteLine("Enter the data to be updated");
                    var data = Console.ReadLine();

                    var property = typeof(Student).GetProperty(col);
                    if (property != null)
                    {
                        var convertedData = Convert.ChangeType(data, property.PropertyType);

                        property.SetValue(studentToBeUpdated, convertedData);

                        context.SaveChanges();

                        Console.WriteLine($"Updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Column Name");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void CourseUpdate()
        {
            try
            {
                Console.WriteLine("Enter the Course ID:");
                int courseId;
                if (!int.TryParse(Console.ReadLine(), out courseId))
                {
                    Console.WriteLine("Invalid Course ID format. Please enter a valid integer.");
                    return;
                }

                var courseToBeUpdated = context.Courses.Find(courseId);

                if (courseToBeUpdated == null)
                {
                    Console.WriteLine("Data not found");
                }
                else
                {
                    Console.WriteLine("Enter the Column to be updated");
                    var col = Console.ReadLine();
                    Console.WriteLine("Enter the data to be updated");
                    var data = Console.ReadLine();

                    var property = typeof(Course).GetProperty(col);
                    if (property != null)
                    {
                        var convertedData = Convert.ChangeType(data, property.PropertyType);

                        property.SetValue(courseToBeUpdated, convertedData);

                        context.SaveChanges();

                        Console.WriteLine($"Updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Column Name");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ClassUpdate()
        {
            try
            {
                Console.WriteLine("Enter the Class ID:");
                int classId;
                if (!int.TryParse(Console.ReadLine(), out classId))
                {
                    Console.WriteLine("Invalid Class ID format. Please enter a valid integer.");
                    return;
                }

                var classToBeUpdated = context.Classes.Find(classId);

                if (classToBeUpdated == null)
                {
                    Console.WriteLine("Data not found");
                }
                else
                {
                    Console.WriteLine("Enter the Column to be updated");
                    var col = Console.ReadLine();
                    Console.WriteLine("Enter the data to be updated");
                    var data = Console.ReadLine();

                    var property = typeof(Class).GetProperty(col);
                    if (property != null)
                    {
                        var convertedData = Convert.ChangeType(data, property.PropertyType);

                        property.SetValue(classToBeUpdated, convertedData);

                        context.SaveChanges();

                        Console.WriteLine($"Updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Column Name");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void StudentDelete()
        {
            try
            {
                Console.WriteLine("Enter the Student ID:");
                int studentId;
                if (!int.TryParse(Console.ReadLine(), out studentId))
                {
                    Console.WriteLine("Invalid Student ID format. Please enter a valid integer.");
                    return;
                }

                var studentToBeDeleted = context.Students.Find(studentId);

                if (studentToBeDeleted == null)
                {
                    Console.WriteLine($"{studentToBeDeleted.StudentID} not found");
                }
                else
                {
                    context.Students.Remove(studentToBeDeleted);
                    context.SaveChanges();
                    Console.WriteLine("Student deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void CourseDelete()
        {
            try
            {
                Console.WriteLine("Enter the Course id:");
                int courseId;
                if (!int.TryParse(Console.ReadLine(), out courseId))
                {
                    Console.WriteLine("Invalid Course Id..Please enter a valid integer");
                    return;
                }

                var courseToBeDeleted = context.Courses.Find(courseId);

                if (courseToBeDeleted == null)
                {
                    Console.WriteLine("Data not found");
                }
                else
                {
                    context.Courses.Remove(courseToBeDeleted);
                    context.SaveChanges();
                    Console.WriteLine("Course deleted successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ClassDelete()
        {
            try
            {
                Console.WriteLine("Enter the Class id:");
                int classId;
                if (!int.TryParse(Console.ReadLine(), out classId))
                {
                    Console.WriteLine("Invalid Class Id..Please enter a valid integer");
                    return;
                }

                var classToBeDeleted = context.Classes.Find(classId);

                if (classToBeDeleted == null)
                {
                    Console.WriteLine("Data not found");
                }
                else
                {
                    context.Classes.Remove(classToBeDeleted);
                    context.SaveChanges();
                    Console.WriteLine("Class deleted successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}