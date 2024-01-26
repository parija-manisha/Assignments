using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Creating a Queue of integers
        Queue<int> myQueue = new Queue<int>();

        // Enqueue (add) elements to the queue
        myQueue.Enqueue(10);
        myQueue.Enqueue(20);
        myQueue.Enqueue(30);
        myQueue.Enqueue(40);

        // Displaying elements in the queue
        Console.WriteLine("Elements in the Queue:");

        foreach (var item in myQueue)
        {
            Console.WriteLine(item);
        }

        int peekedItem = myQueue.Peek();
        Console.WriteLine($"\nPeeked item: {peekedItem}");

        // Dequeue (remove) an element from the front of the queue
        int removedItem = myQueue.Dequeue();
        Console.WriteLine($"\nDequeued item: {removedItem}");

        // Displaying the updated queue
        Console.WriteLine("\nElements in the Queue after Dequeue:");

        foreach (var item in myQueue)
        {
            Console.WriteLine(item);
        }

        Console.ReadLine(); // Keep the console window open until a key is pressed
    }
}

