using System;
using System.Data.SqlClient;

namespace Practice_dbConnection
{
    internal class Program
    {
        static void Main()
        {
            SqlConnection conn = new SqlConnection("Data Source = MANISHAP-WIN10; Initial Catalog = AdventureWorks2022; Integrated Security = True;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM HumanResources.Department", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                // Print column headers
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader.GetName(i).PadRight(20));
                }
                Console.WriteLine();

                // Print a separator line
                Console.WriteLine(new string('-', 20 * reader.FieldCount));

                // Print data rows
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i].ToString().PadRight(20));
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }

            reader.Close();

            conn.Close();
        }
    }
}
