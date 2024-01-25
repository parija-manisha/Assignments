using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    internal class FileRead
    {
        static void Main(string[] args)
        {
            bool exit = false;

            do
            {
                Console.WriteLine("1. Open a new file");
                Console.WriteLine("2. Open an existing file");
                Console.WriteLine("3. Read a file");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Choose an option (1/2/3/4): ");
                string input = Console.ReadLine();
                string filename;
                string dataAdded;

                switch (input)
                {
                    case "3":
                        Console.WriteLine("Enter the filename:");
                        filename = Console.ReadLine();

                        if (File.Exists(filename))
                        {
                            string str = File.ReadAllText(filename);
                            Console.WriteLine("Contents: ");
                            Console.WriteLine(str);
                        }
                        break;

                    case "2":
                        Console.WriteLine("Enter the filename:");
                        filename = Console.ReadLine();
                        Console.WriteLine("Enter the data to be added:");
                        dataAdded = Console.ReadLine();
                        using (StreamWriter sw = File.AppendText(filename))
                        {
                            sw.WriteLine(dataAdded);
                        }
                        break;

                    case "1":
                        Console.WriteLine("Enter the filename:");
                        filename = Console.ReadLine();
                        Console.WriteLine("Enter the data to be added:");
                        dataAdded = Console.ReadLine();
                        using (StreamWriter sw = File.CreateText(filename))
                        {
                            sw.WriteLine(dataAdded);
                        }
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            } while (!exit);

            Console.WriteLine("Application is now closed.");
        }
    }
}
