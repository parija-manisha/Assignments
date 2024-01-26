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

            // Define the SQL query for creating a view
            string createViewQuery = "CREATE VIEW [dbo].[DEMO_DATABASE_CONNECTION_VIEW] AS " +
                                     "SELECT * FROM DEMO_DATABASE_CONNECTION";

            using (SqlCommand command = new SqlCommand(createViewQuery, connection))
            {
                // Execute the query to create the view
                command.ExecuteNonQuery();
                Console.WriteLine("View created successfully");
            }
        }

        Console.ReadLine(); // Keep the console window open until a key is pressed
    }
}
