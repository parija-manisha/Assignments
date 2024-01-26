using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Polymorphism
{
    public class Animal
    {
        public string Name { get; set; }
        public virtual void MakeSound()
        {
            Console.WriteLine("Some generic animal sound.");
        }
    }

    public class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} Woof! Woof!");
        }
    }

    public class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} Meow! Meow!");
        }
    }

    internal class Program
    {
        static void Main()
        {
            Animal myAnimal;

            myAnimal = new Dog();
            myAnimal.Name = "Buddy";
            myAnimal.MakeSound(); 

            myAnimal = new Cat();
            myAnimal.Name = "Whiskers";
            myAnimal.MakeSound();

            Console.ReadLine();
        }
    }
}