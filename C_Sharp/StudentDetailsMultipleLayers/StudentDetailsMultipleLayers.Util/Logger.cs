using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Util
{
    public class Logger
    {
        public static void AddError(string text, Exception ex)
        {
            string logMessage = $"Exception Details\n{ex.ToString()}";

            string filename = $"{DateTime.Now:dd-MM-yyyy--hh-mm-ss}.txt";
            string filepath = Path.Combine(ConfigurationManager.AppSettings["LogFilePath"], filename);

            // Write to file
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(logMessage);
            }

            //Insert into database
                using (var context = Connection.ConnectADO())
            {
                context.Open();
                SqlCommand command = new SqlCommand("INSERT INTO LogTable (LogName, LogDate) VALUES (@log, @date)", context);

                command.Parameters.AddWithValue("@log", ex.Message);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.ExecuteNonQuery();
            }
        }
    }

}
