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

    public static DataTable ToDataTable<T>(this List<T> list)
    {
        DataTable dataTable = new DataTable();

        // If the list is empty, return an empty DataTable
        if (list.Count == 0)
            return dataTable;

        // Get the type of the objects in the list
        Type objectType = typeof(T);

        // Get the properties of the object type
        var properties = objectType.GetProperties();

        // Add columns to the DataTable based on the object properties
        foreach (var property in properties)
        {
            dataTable.Columns.Add(property.Name, property.PropertyType);
        }

        // Add rows to the DataTable based on the object values
        foreach (var item in list)
        {
            DataRow row = dataTable.NewRow();

            foreach (var property in properties)
            {
                row[property.Name] = property.GetValue(item);
            }

            dataTable.Rows.Add(row);
        }

        return dataTable;
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public DataTable ToDataTable()
        {
            List<Person> list = new List<Person> { this };
            return list.ToDataTable();
        }
    }

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
}
