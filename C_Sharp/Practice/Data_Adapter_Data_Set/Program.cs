using System;
using System.Data;
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

            string query = "SELECT * FROM DEMO_DATABASE_CONNECTION";

            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "DEMO_DATABASE_CONNECTION");

                DataTable dataTable = dataSet.Tables["DEMO_DATABASE_CONNECTION"];

                foreach (DataRow row in dataTable.Rows)
                {
                    string Name = row["Name"].ToString();
                    string Age = row["Age"].ToString();

                    Console.WriteLine($"Name: {Name} Age: {Age}");
                }
            }
        }

        Console.ReadLine();
    }
}
