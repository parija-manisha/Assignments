using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Database_Connection
{
    internal class Program
    {
        static void Main()
        {
            string connectionString = "Data Source = MANISHAP-WIN10; Initial Catalog = TEST; Integrated Security = True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Enter Name");
                string name = Console.ReadLine();

                Console.WriteLine("Enter age");
                int age = Convert.ToInt32(Console.ReadLine());

                string insert = "INSERT INTO Demo_Database_Connection VALUES (@Name, @Age)";
                using (SqlCommand command = new SqlCommand(insert, conn))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Age", age);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Data Added Successfully");
                    }
                    else
                    {
                        Console.WriteLine("Data Addition Failed");
                    }
                }

                string display = "SELECT * FROM Demo_Database_Connection";
                using(SqlCommand command = new SqlCommand(display, conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) {
                        for(int i = 0; i< reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetName(i).PadRight(20));
                        }
                        Console.WriteLine();

                        Console.WriteLine(new String('-', 20*reader.FieldCount));

                        while (reader.Read())
                        {
                            for(int i = 0;i< reader.FieldCount;i++)
                            {
                                Console.Write(reader[i].ToString().PadRight(20));
                            }
                            Console.WriteLine();
                        }
                    }
                    else { 
                        Console.WriteLine("No rows found!"); 
                    }
                }
            }
        }
    }
}
