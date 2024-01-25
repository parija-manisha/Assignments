using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //int age = 10;
            //Console.WriteLine(age);

            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine(username);

            Console.WriteLine("Enter age:");
            //int age = Console.ReadLine();  returns error
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(age);
        }
    }
}
