using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = Enumerable.Range(1, 100).ToArray();
        var result = numbers.AsParallel()
                            .WithDegreeOfParallelism(4)
                            .Select(x => x * x)
                            .Sum();

        Console.WriteLine($"Sum of squares: {result}");

        Console.ReadLine();
    }
}
