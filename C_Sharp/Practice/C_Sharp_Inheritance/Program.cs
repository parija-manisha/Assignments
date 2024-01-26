using System;

namespace C_Sharp_Inheritance
{
    public class Animal
    {
        public string Name { get; set; }

        public void Eat()
        {
            Console.WriteLine($"{Name} is eating.");
        }
    }

    public class Dog : Animal
    {
        public void Bark()
        {
            Console.WriteLine($"{Name} is barking.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Dog myDog = new Dog();
            myDog.Name = "Buddy";

            Console.WriteLine($"Name: {myDog.Name}");
            myDog.Eat();
            myDog.Bark();

            Console.ReadLine();
        }
    }
}
