using System;
using System.Threading;

class Program
{
    static int counter = 0;
    static Mutex mutex = new Mutex();

    static void Main()
    {
        // Create two threads and assign the IncrementCounter method to each thread
        Thread thread1 = new Thread(IncrementCounter);
        Thread thread2 = new Thread(IncrementCounter);

        // Start the threads
        thread1.Start();
        thread2.Start();

        // Wait for both threads to finish before ending the program
        thread1.Join();
        thread2.Join();

        Console.WriteLine($"Final counter value: {counter}");
        Console.WriteLine("Main thread exiting.");
    }

    static void IncrementCounter()
    {
        for (int i = 0; i < 100000; i++)
        {
            // Use a Mutex to ensure that only one thread can access the shared resource at a time
            mutex.WaitOne();

            try
            {
                // Increment the shared counter
                counter++;
            }
            finally
            {
                // Release the Mutex
                mutex.ReleaseMutex();
            }
        }
    }
}
