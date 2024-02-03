using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_DB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String connection = "data Source=MANISHAP-WIN10;Initial Catalog=TEST;Integrated Security=True";
            using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                Console.WriteLine("Connected");
            }
        }
    }
}
