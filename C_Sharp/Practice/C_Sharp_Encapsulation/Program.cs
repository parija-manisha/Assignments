using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Encapsulation
{
    public class Person
    {
        private string name;
        private int age;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value >= 0)
                {
                    age = value;
                }
                else
                {
                    Console.WriteLine("Age cannot be negative.");
                }
            }
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
        }
    }

    class Program
    {
        static void Main()
        {
            Person person = new Person();

            person.Name = "John Doe";
            person.Age = -30;

            person.PrintDetails();

            Console.ReadLine();
        }
    }
}