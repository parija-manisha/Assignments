using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=MANISHAP-WIN10;Initial Catalog=TEST;Integrated Security=True";

        SelectOperation(connectionString);

        InsertOperation(connectionString);

        UpdateOperation(connectionString);

        DeleteOperation(connectionString);

        Console.ReadLine(); 
    }

    static void SelectOperation(string connectionString)
    {
        Console.WriteLine("Select Operation:");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string selectQuery = "SELECT * FROM DEMO_DATABASE_CONNECTION";

            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Name: {reader["Name"]}, Age: {reader["Age"]}");
                }
            }
        }

        Console.WriteLine();
    }

    static void InsertOperation(string connectionString)
    {
        Console.WriteLine("Insert Operation:");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Replace with your INSERT query
            string insertQuery = "INSERT INTO DEMO_DATABASE_CONNECTION (Name, Age) VALUES ('ALISHA', 20)";

            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Rows Inserted: {rowsAffected}");
            }
        }

        Console.WriteLine();
    }

    static void UpdateOperation(string connectionString)
    {
        Console.WriteLine("Update Operation:");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Replace with your UPDATE query
            string updateQuery = "UPDATE DEMO_DATABASE_CONNECTION SET Age = 31 WHERE Name = 'MANISHA'";

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Rows Updated: {rowsAffected}");
            }
        }

        Console.WriteLine();
    }

    static void DeleteOperation(string connectionString)
    {
        Console.WriteLine("Delete Operation:");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string deleteQuery = "DELETE FROM DEMO_DATABASE_CONNECTION WHERE Name = 'MANISHA'";

            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Rows Deleted: {rowsAffected}");
            }
        }

        Console.WriteLine();
    }
}
