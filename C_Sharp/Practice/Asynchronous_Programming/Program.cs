using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Starting...");

        // Call the asynchronous method
        await GetDataAsync();

        Console.WriteLine("Program completed.");
        Console.ReadLine(); // Keep the console window open until a key is pressed
    }

    static async Task GetDataAsync()
    {
        // Example: Asynchronous HTTP request using HttpClient
        using (HttpClient client = new HttpClient())
        {
            // Asynchronously download the content of a web page
            string result = await client.GetStringAsync("https://www.w3schools.com");

            // Display the result after completion
            Console.WriteLine($"Length of the downloaded content: {result.Length} characters");
        }
    }
}
