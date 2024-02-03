using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace StudentDetailsLayers.Util
{
    public class Logger
    {
        public static void AddError(string message, Exception exception)
        {
            string logMessage = $"{DateTime.Now}: {message}\nException details: {exception}";
            Console.WriteLine(logMessage);

            LogToFile(logMessage);
        }

        private static void LogToFile(string logMessage)
        {
            try
            {
                //string fileName = $"{DateTime.Now:yyyy/MM/dd_HH-mm-ss}.txt";
                //string filePath = Path.Combine(ConfigurationManager.AppSettings["LogFilePath"], fileName);

                //Directory.CreateDirectory(ConfigurationManager.AppSettings["LogFilePath"]);

                //using (StreamWriter writer = new StreamWriter(filePath, true))
                //{
                //    writer.WriteLine(logMessage);
                //}

                using (SqlConnection con = Connection.Connected())
                {
                    SqlCommand cm = new SqlCommand("insert into logtable values (@log, @date)", con);

                    cm.Parameters.AddWithValue("@log", logMessage);
                    cm.Parameters.AddWithValue("@date", DateTime.Now);

                    con.Open();
                    cm.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while writing to log file or database: {ex}");
            }
        }
    }
}
