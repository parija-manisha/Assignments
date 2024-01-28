using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public static class DataTableExtensions
{
    public static List<T> ToList<T>(this DataTable dataTable) where T : new()
    {
        if (dataTable == null)
            throw new ArgumentNullException(nameof(dataTable), "DataTable cannot be null.");

        List<T> result = new List<T>();

        try
        {
            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();

                foreach (var property in typeof(T).GetProperties())
                {
                    if (dataTable.Columns.Contains(property.Name) && row[property.Name] != DBNull.Value)
                    {
                        property.SetValue(obj, row[property.Name]);
                    }
                }

                result.Add(obj);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error converting DataTable to List of Objects: {ex.Message}");
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=MANISHAP-WIN10;Initial Catalog=TEST;Integrated Security=True";

        Console.Write("Enter the table name: ");
        string tableName = Console.ReadLine();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $"SELECT * FROM {tableName}";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                List<Person> people = dataTable.ToList<Person>();

                foreach (var person in people)
                {
                    Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
                }
            }
        }

        Console.ReadLine();
    }
    public class Person
    { 
        public string Name { get; set; }
        public int Age { get; set; }
    }
}