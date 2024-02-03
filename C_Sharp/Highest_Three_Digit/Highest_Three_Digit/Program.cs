using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highest_Three_Digit
{
    internal class Program
    {
        static void Main()
        {
            var test = "23,45,21|09,33,98,,36,89,-11,09,4,100.5|33,89";
            var sum = test.Split(new char[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(s => double.Parse(s))
                                   .Distinct()
                                   .OrderByDescending(num => num)
                                   .Take(3)
                                   .Sum();

            Console.WriteLine($"Sum: {sum}");

        }
    }
}
