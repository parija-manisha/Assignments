using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=MANISHAP-WIN10;Initial Catalog=TEST;Integrated Security=True";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string createIndexQuery = "CREATE INDEX IX_NAME ON DEMO_DATABASE_CONNECTION(NAME)";

            using (SqlCommand command = new SqlCommand(createIndexQuery, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Index created successfully");
            }
        }

        Console.ReadLine();
    }
}
