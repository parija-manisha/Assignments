using StudentDetailsMultipleLayers.Business;
using StudentDetailsMultipleLayers.Model;
using StudentDetailsMultipleLayers.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.API
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            do
            {
                Console.WriteLine("Enter the table Choice:\n 1.Student \n 2.Course \n 3.Class \n 4.Student_ClassRelationship \n 5.Course_ClassRelationship \n 6.Exit");
                if (Utility.TryParseEnum(Console.ReadLine(), out EnumValues.MenuChoiceTable choiceTable))
                {
                    if (choiceTable == EnumValues.MenuChoiceTable.Exit)
                    {
                        break;
                    }
                    Console.WriteLine("Enter the operation Choice:\n 1.Insert \n 2.Update \n 3.Delete \n 4.Display");
                    if (Utility.TryParseEnum(Console.ReadLine(), out EnumValues.MenuChoiceOperation choiceOperation))
                    {
                        switch (choiceTable)
                        {
                            case EnumValues.MenuChoiceTable.Student
                                :
                                HandleStudentOperation(choiceOperation);
                                break;
                            case EnumValues.MenuChoiceTable.Course
                                :
                                HandleCourseOperation(choiceOperation);
                                break;
                            case EnumValues.MenuChoiceTable.Class
                                :
                                HandleClassOperation(choiceOperation);
                                break;
                            case EnumValues.MenuChoiceTable.StudentClass
                                :
                                HandleStudentClassOperation(choiceOperation);
                                break;
                            case EnumValues.MenuChoiceTable.CourseClass
                                :
                                HandleCourseClassOperation(choiceOperation);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Choice");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Choice");
                }

            } while (!exit);
        }

        /// <summary>
        /// Function to choose operation for Student Table
        /// </summary>
        /// <param name="choiceOperation"></param>
        static void HandleStudentOperation(EnumValues.MenuChoiceOperation choiceOperation)
        {
            switch (choiceOperation)
            {
                case EnumValues.MenuChoiceOperation.Insert
                    :
                    InsertStudent(typeof(StudentDTO));
                    break;
                case EnumValues.MenuChoiceOperation.Update
                    :
                    UpdateStudent();
                    break;
                case EnumValues.MenuChoiceOperation.Delete
                    :
                    DeleteStudent();
                    break;
                case EnumValues.MenuChoiceOperation.Display
                    :
                    DisplayStudent();
                    break;
            }
        }

        /// <summary>
        /// Function to choose operation for Course Table
        /// </summary>
        /// <param name="choiceOperation"></param>
        static void HandleCourseOperation(EnumValues.MenuChoiceOperation choiceOperation)
        {
            switch (choiceOperation)
            {
                case EnumValues.MenuChoiceOperation.Insert
                    :
                    InsertCourse(typeof(CourseDetailDTO));
                    break;
                case EnumValues.MenuChoiceOperation.Update
                    :
                    UpdateCourse();
                    break;
                case EnumValues.MenuChoiceOperation.Delete
                    :
                    DeleteCourse();
                    break;
                case EnumValues.MenuChoiceOperation.Display
                    :
                    DisplayCourse();
                    break;
            }
        }

        /// <summary>
        /// Function to choose operation for Class Table
        /// </summary>
        /// <param name="choiceOperation"></param>
        static void HandleClassOperation(EnumValues.MenuChoiceOperation choiceOperation)
        {
            switch (choiceOperation)
            {
                case EnumValues.MenuChoiceOperation.Insert
                    :
                    InsertClass(typeof(ClassDetailDTO));
                    break;
                case EnumValues.MenuChoiceOperation.Update
                    :
                    UpdateClass();
                    break;
                case EnumValues.MenuChoiceOperation.Delete
                    :
                    DeleteClass();
                    break;
                case EnumValues.MenuChoiceOperation.Display
                    :
                    DisplayClass();
                    break;
            }
        }

        /// <summary>
        /// Function to choose operation for StudentClass Table
        /// </summary>
        /// <param name="choiceOperation"></param>
        static void HandleStudentClassOperation(EnumValues.MenuChoiceOperation choiceOperation)
        {
            switch (choiceOperation)
            {
                case EnumValues.MenuChoiceOperation.Insert
                    :
                    AssignStudentToClass(typeof(StudentClassDTO));
                    break;
                case EnumValues.MenuChoiceOperation.Update
                    :
                    Console.WriteLine("You are not allowed to update data into this table. Opt for display to see the data");
                    break;
                case EnumValues.MenuChoiceOperation.Delete
                    :
                    Console.WriteLine("You are not allowed to delete data into this table. Opt for display to see the data");
                    break;
                case EnumValues.MenuChoiceOperation.Display
                    :
                    DisplayStudentClass();
                    break;
            }
        }

        /// <summary>
        /// Function to choose operation for CourseClass Table
        /// </summary>
        /// <param name="choiceOperation"></param>
        static void HandleCourseClassOperation(EnumValues.MenuChoiceOperation choiceOperation)
        {
            switch (choiceOperation)
            {
                case EnumValues.MenuChoiceOperation.Insert
                    :
                    AssignCourseToClass();
                    break;
                case EnumValues.MenuChoiceOperation.Update
                    :
                    Console.WriteLine("You are not allowed to update data into this table. Opt for display to see the data");
                    break;
                case EnumValues.MenuChoiceOperation.Delete
                    :
                    Console.WriteLine("You are not allowed to delete data into this table. Opt for display to see the data");
                    break;
                case EnumValues.MenuChoiceOperation.Display
                    :
                    //DisplayCourseClass();
                    break;
            }
        }

        /// <summary>
        /// Function to Insert into student Table
        /// </summary>
        /// <param name="entityType"></param>
        static void InsertStudent(Type entityType)
        {
            try
            {
                var entity = Activator.CreateInstance(entityType);
                var properties = entityType.GetProperties();
                bool allColumnsNotEmpty = true;

                foreach (var property in properties)
                {
                    Console.WriteLine($"Enter {property.Name}");
                    string input = Console.ReadLine();

                    if (String.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine($"\n{property.Name} cannot be null\n");
                        allColumnsNotEmpty = false;
                        break;
                    }

                    if (property.PropertyType == typeof(int))
                    {
                        if (!Utility.TryParseInt(input, out int intValue))
                        {
                            Console.WriteLine($"Invalid {property.Name} format. Please enter a valid integer.");
                            allColumnsNotEmpty = false;
                            break;
                        }
                        property.SetValue(entity, intValue, null);
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(entity, input, null);
                    }
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        if (!Utility.TryParseDate(input, out DateTime dateValue))
                        {
                            Console.WriteLine($"Invalid {property.Name} format. Please enter a valid date.");
                            allColumnsNotEmpty = false;
                            break;
                        }
                        property.SetValue(entity, dateValue.ToString());
                    }
                }

                if (allColumnsNotEmpty)
                {
                    bool success = StudentLogic.AddStudent((StudentDTO)entity);

                    if (success)
                    {
                        Console.WriteLine("\nStudent added Successfully\n");
                    }
                    else {
                        Console.WriteLine("\nFailed to add Student\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nFailed to add Student\n", ex.InnerException);
            }
        }

        /// <summary>
        /// Function to Insert into course Table
        /// </summary>
        /// <param name="entityType"></param>
        static void InsertCourse(Type entityType)
        {
            try
            {
                var entity = Activator.CreateInstance(entityType);
                var properties = entityType.GetProperties();

                foreach (var property in properties)
                {
                    Console.WriteLine($"Enter {property.Name}");
                    string input = Console.ReadLine();

                    if (input != null)
                    {
                        if (property.PropertyType == typeof(int))
                        {
                            if (!Utility.TryParseInt(input, out int intValue))
                            {
                                Console.WriteLine($"Invalid {property.Name} format. Please enter a valid integer.");
                                continue;
                            }
                            property.SetValue(entity, intValue, null);
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(entity, input, null);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{property.Name} cannot be null");
                        continue;
                    }
                }

                bool success = CourseLogic.AddCourse((CourseDetailDTO)entity);

                if (success)
                {
                    Console.WriteLine("\nCourse added Successfully\n");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nFailed to add Course\n", ex.InnerException);
            }
        }

        /// <summary>
        /// Function to Insert into class Table
        /// </summary>
        /// <param name="entityType"></param>
        static void InsertClass(Type entityType)
        {
            try
            {
                var entity = Activator.CreateInstance(entityType);
                var properties = entityType.GetProperties();

                foreach (var property in properties)
                {
                    Console.WriteLine($"Enter {property.Name}");
                    string input = Console.ReadLine();

                    if (!String.IsNullOrWhiteSpace(input))
                    {
                        if (property.PropertyType == typeof(int))
                        {
                            if (!Utility.TryParseInt(input, out int intValue))
                            {
                                Console.WriteLine($"Invalid {property.Name} format. Please enter a valid integer.");
                            }
                            property.SetValue(entity, intValue, null);
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(entity, input, null);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{property.Name} cannot be null");
                    }
                }

                bool success = ClassLogic.AddClass((ClassDetailDTO)entity);

                if (success)
                {
                    Console.WriteLine("\nClass added Successfully\n");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nFailed to add Class\n", ex);
            }
        }

        /// <summary>
        /// Function to Assign student to class Table
        /// </summary>
        static void AssignStudentToClass(Type entityType)
        {
            try
            {
                var entity = Activator.CreateInstance(entityType);
                var properties = entityType.GetProperties();

                foreach (var property in properties)
                {
                    Console.WriteLine($"Enter {property.Name}");
                    string input = Console.ReadLine();

                    if (input != null)
                    {
                        if (property.PropertyType == typeof(int))
                        {
                            if (!Utility.TryParseInt(input, out int intValue))
                            {
                                Console.WriteLine($"Invalid {property.Name} format. Please enter a valid integer.");
                                continue;
                            }
                            property.SetValue(entity, intValue, null);
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(entity, input, null);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{property.Name} cannot be null");
                        continue;
                    }
                }

                bool success = StudentClassLogic.AssignStudentToClass((StudentClassDTO)entity);

                if (success)
                {
                    Console.WriteLine("\nStudent assigned to class added Successfully\n");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nFailed to assign Student to Class\n", ex.InnerException);
            }
        }

        /// <summary>
        /// Function to Assign course to class Table
        /// </summary>
        static void AssignCourseToClass()
        {
            Console.WriteLine("Enter Course ID:");
            int courseId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Class ID:");
            int classId = int.Parse(Console.ReadLine());
            if (CourseLogic.AssignCourseToClass(courseId, classId))
            {
                Console.WriteLine("Course assigned to class successfully.");
            }
            else
            {
                Console.WriteLine("Assigning Course to Class failed");
            }
        }

        /// <summary>
        /// Function to Update into student Table
        /// </summary>
        static void UpdateStudent()
        {
            try
            {
                Console.WriteLine("Enter the Student ID you want to update");
                int studentID = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Enter the Column to be Updated");
                string col = Console.ReadLine();

                Console.WriteLine("Enter the data to be updated");
                string data = Console.ReadLine();

                bool addSuccess = StudentLogic.ModifyStudent(studentID, col, data);

                if (addSuccess)
                {
                    Console.WriteLine($"\n{col} updated successfully!\n");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed. \n Error:", ex);
            }
        }

        /// <summary>
        /// Function to Update into course Table
        /// </summary>
        static void UpdateCourse()
        {
            try
            {
                Console.WriteLine("Enter the Student ID you want to update");
                int courseID = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Enter the Column to be Updated");
                string col = Console.ReadLine();

                Console.WriteLine("Enter the data to be updated");
                string data = Console.ReadLine();

                bool addSuccess = CourseLogic.ModifyCourse(courseID, col, data);

                if (addSuccess)
                {
                    Console.WriteLine($"\n{col} updated successfully!\n");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed. \n Error:", ex);
            }
        }

        /// <summary>
        /// Function to Update into class Table
        /// </summary>
        static void UpdateClass()
        {
            try
            {
                Console.WriteLine("Enter the Class ID you want to update");
                int classID = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Enter the Column to be Updated");
                string col = Console.ReadLine();

                Console.WriteLine("Enter the data to be updated");
                string data = Console.ReadLine();

                bool addSuccess = ClassLogic.ModifyClass(classID, col, data);

                if (addSuccess)
                {
                    Console.WriteLine($"\n{col} updated successfully!\n");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Updation Failed. \n Error:", ex);
            }
        }

        /// <summary>
        /// Function to Delete into student Table
        /// </summary>
        static void DeleteStudent()
        {
            try
            {
                Console.WriteLine("Enter the Student ID you want to delete");
                int studentID = Convert.ToInt32(Console.ReadLine().Trim());

                bool addSuccess = StudentLogic.RemoveStudent(studentID);

                if (addSuccess)
                {
                    Console.WriteLine($"\nClass id {studentID} deleted successfully!\n");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Deleteion Failed. \n Error:", ex);
            }
        }

        /// <summary>
        /// Function to Delete into course Table
        /// </summary>
        static void DeleteCourse()
        {
            try
            {
                Console.WriteLine("Enter the Course ID you want to delete");
                int courseID = Convert.ToInt32(Console.ReadLine().Trim());

                bool addSuccess = CourseLogic.RemoveCourse(courseID);

                if (addSuccess)
                {
                    Console.WriteLine($"\nClass id {courseID} deleted successfully!\n");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Deleteion Failed. \n Error:", ex);
            }
        }

        /// <summary>
        /// Function to Delete into class Table
        /// </summary>
        static void DeleteClass()
        {
            try
            {
                Console.WriteLine("Enter the Class ID you want to delete");
                int classID = Convert.ToInt32(Console.ReadLine().Trim());

                bool deleteSuccess = ClassLogic.RemoveClass(classID);

                if (deleteSuccess)
                {
                    Console.WriteLine($"\nClass with ID {classID} deleted successfully!\n");
                }
                else
                {
                    Console.WriteLine($"\nFailed to delete class with ID {classID}\n");
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("\nDeletion Failed. \n Error:", ex);
            }
        }

        /// <summary>
        /// Function to Display into Student Table
        /// </summary>
        static void DisplayStudent()
        {
            List<StudentDTO> allStudents = StudentLogic.DisplayStudent();
            Console.WriteLine("\nStudents:\n", Console.Title);
            foreach (var student in allStudents)
            {
                Console.WriteLine($"ID: {student.StudentID}, Name: {student.StudentName}, Gender: {student.Gender}, PhoneNumber: {student.PhoneNumber}, DateOfBirth: {student.DateOfBirth}, Email: {student.Email}\n");
            }
        }

        /// <summary>
        /// Function to Display into Course Table
        /// </summary>
        static void DisplayCourse()
        {
            List<CourseDetailDTO> allCourses = CourseLogic.DisplayCourse();
            Console.WriteLine("\nCourses:\n");
            foreach (var course in allCourses)
            {
                Console.WriteLine($"ID: {course.CourseID}, Name: {course.CourseName}\n");
            }
        }

        /// <summary>
        /// Function to Display into Class Table
        /// </summary>
        static void DisplayClass()
        {
            List<ClassDetailDTO> allClasses = ClassLogic.DisplayClass();
            Console.WriteLine("\nCourses:\n");
            foreach (var newClass in allClasses)
            {
                Console.WriteLine($"ID: {newClass.ClassID}, Name: {newClass.ClassName}\n");
            }
        }

        /// <summary>
        /// Function to Display into StudentClass Table
        /// </summary>
        static void DisplayStudentClass()
        {
            StudentClassLogic studentClassLogic = new StudentClassLogic();
            var students = studentClassLogic.DisplayStudentClass();

            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentID}, Name: {student.ClassID}, Class: {student.ClassName}");
            }

        }

    }
}

