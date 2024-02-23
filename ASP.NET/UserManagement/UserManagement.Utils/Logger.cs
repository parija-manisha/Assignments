using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Utils
{
    public class Logger
    {
        public static void AddError(string text, Exception ex)
        {
            string logMessage = $"Exception Details\n{GetExceptionDetails(ex)}";

            string fileName = $"{DateTime.Now:dd-MM-yyyy--hh-mm-ss}.txt";
            string filePath = Path.Combine(ConfigurationManager.AppSettings["LogFilePath"], fileName);

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(logMessage);
            }
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
