using System;
using System.Collections.Generic;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Creating a dictionary with string keys and int values
        Dictionary<string, int> ageDictionary = new Dictionary<string, int>();

        // Adding key/value pairs to the dictionary
        //ageDictionary["John"] = 25;
        //ageDictionary["Alice"] = 30;
        //ageDictionary["Bob"] = 22;

        while (true)
        {
            Console.WriteLine("Enter Name:");
            string name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
                break;

            Console.WriteLine("Enter Age:");
            int age = Convert.ToInt32(Console.ReadLine());

            ageDictionary[name] = age;
        }

        // Accessing values using keys
        Console.WriteLine("Age of Manisha: " + ageDictionary["Manisha"]);

        // Iterating through key/value pairs
        Console.WriteLine("All Key/Value Pairs:");
        foreach (var kvp in ageDictionary)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        // Checking if a key exists in the dictionary
        string nameToCheck = "Alice";
        if (ageDictionary.ContainsKey(nameToCheck))
        {
            Console.WriteLine($"{nameToCheck} exists in the dictionary.");
        }
        else
        {
            Console.WriteLine($"{nameToCheck} does not exist in the dictionary.");
        }

        foreach (var kvp in ageDictionary)
        {
        if(ageDictionary.TryGetValue(kvp.Key, out int age))
            {
                Console.WriteLine(age + " " + kvp.Key);   
            }
        }

        // Removing a key/value pair
        ageDictionary.Remove("Bob");

        Console.WriteLine("After removing Bob:");
        foreach (var kvp in ageDictionary)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        Console.ReadLine();
    }
}
