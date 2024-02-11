using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Util
{
    public class Logger
    {
        public static void AddError(string text, Exception ex)
        {
            string logMessage = $"Exception Details\n{GetExceptionDetails(ex)}";

            string filename = $"{DateTime.Now:dd-MM-yyyy--hh-mm-ss}.txt";
            string filepath = Path.Combine(ConfigurationManager.AppSettings["LogFilePath"], filename);

            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(logMessage);
            }

            //using (var context = Connection.Connect())
            //{
            //    context.Open();
            //    SqlCommand command = new SqlCommand("INSERT INTO LogTable (LogName, LogDate) VALUES (@log, @date)", context);

            //    command.Parameters.AddWithValue("@log", ex);
            //    command.Parameters.AddWithValue("@date", DateTime.Now);
            //    command.ExecuteNonQuery();
            //}
        }

        private static string GetExceptionDetails(Exception ex)
        {
            StringBuilder details = new StringBuilder();
            Exception currentException = ex;

            while (currentException != null)
            {
                details.AppendLine($"Message: {currentException.Message}");
                details.AppendLine($"Source: {currentException.Source}");
                details.AppendLine($"Stack Trace: {currentException.StackTrace}");
                details.AppendLine();

                currentException = currentException.InnerException;
            }

            return details.ToString();
        }
    }
}
