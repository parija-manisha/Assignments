using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Create two threads and assign the PrintNumbers method to each thread
        Thread thread1 = new Thread(PrintNumbers);
        Thread thread2 = new Thread(PrintNumbers);

        // Start the threads
        thread1.Start();
        thread2.Start();

        // Wait for both threads to finish before ending the program
        thread1.Join();
        thread2.Join();

        Console.WriteLine("Main thread exiting.");
    }

    static void PrintNumbers()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: {i}");
            Thread.Sleep(100); // Simulate some work
        }
    }
}
