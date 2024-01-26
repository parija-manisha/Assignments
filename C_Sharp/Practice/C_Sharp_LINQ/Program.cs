using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        //List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        //var evenNumbers = from number in numbers
        //                  where number % 3 == 0
        //                  select number;

        //Console.WriteLine("Even Numbers:");
        //foreach (var number in evenNumbers)
        //{
        //    Console.WriteLine(number);
        //}

        //Console.ReadLine(); 
       
        
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var evenNumbers = numbers.Where(number => number % 2 == 0);

        Console.WriteLine("Even Numbers:");
        foreach (var number in evenNumbers)
        {
            Console.WriteLine(number);
        }

        Console.ReadLine();
    }
}