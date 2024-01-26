using System;
using System.Collections.Generic;

namespace C_Sharp_List
{
    internal class Program
    {
        private static void IterateThroughLists()
        {
            var theGalaxies = new List<Galaxy>
            {
                new Galaxy { Name = "Tadpole", MegaLightYears = 400 },
                new Galaxy { Name = "Pinwheel", MegaLightYears = 25 },
                new Galaxy { Name = "Milky Way", MegaLightYears = 0 },
                new Galaxy { Name = "Andromeda", MegaLightYears = 3 }
            };

            Console.WriteLine("Galaxies:");
            foreach (Galaxy theGalaxy in theGalaxies)
            {
                Console.WriteLine($"{theGalaxy.Name}  {theGalaxy.MegaLightYears}");
            }
            Console.WriteLine();
        }

        public class Galaxy
        {
            public string Name { get; set; }
            public int MegaLightYears { get; set; }
        }

        static void Main()
        {
            IterateThroughLists();
            List<string> stringList = new List<string>();

            Console.WriteLine("Enter values:");

            while (true)
            {
                string userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput))
                    break;

                stringList.Add(userInput);
            }

            Console.WriteLine("List:");
            foreach (string item in stringList)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();

            List<int> intList = new List<int>();
            int[] numbers = new int[5] { 1, 2, 3, 4, 5 };

            intList.AddRange(numbers);

            Console.WriteLine("List:");
            foreach (int number in intList)
            {
                Console.WriteLine(number);
            }
        }
    }
}
