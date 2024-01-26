using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Abstaction
{
    public abstract class Shape
    {
        public abstract double CalculateArea();
        public void Display()
        {
            Console.WriteLine("This is a shape.");
        }
    }

    public interface IPrintable
    {
        void Print();
    }

    public class Circle : Shape, IPrintable
    {
        public double Radius { get; set; }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }

        public void Print()
        {
            Console.WriteLine($"Circle with radius {Radius}");
        }
    }

    class Program
    {
        static void Main()
        {
            Circle circle = new Circle();
            circle.Radius = 5;

            circle.Display();          
            double area = circle.CalculateArea();  
            Console.WriteLine($"Area: {area}");

            circle.Print();
            Console.ReadLine();
        }
    }
}
