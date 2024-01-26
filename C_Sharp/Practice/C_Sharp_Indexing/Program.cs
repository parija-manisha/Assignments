using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Replace with your SQL Server connection string
        string connectionString = "Data Source=MANISHAP-WIN10;Initial Catalog=TEST;Integrated Security=True";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Define the SQL query for creating an index
            string createIndexQuery = "CREATE INDEX IX_NAME ON DEMO_DATABASE_CONNECTION(NAME)";

            using (SqlCommand command = new SqlCommand(createIndexQuery, connection))
            {
                // Execute the query to create the index
                command.ExecuteNonQuery();
                Console.WriteLine("Index created successfully");
            }
        }

        Console.ReadLine(); // Keep the console window open until a key is pressed
    }
}
