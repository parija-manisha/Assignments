using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AirportFuelInventory.Utils
{
    public class Logger
    {
        public static void AddError(string text, Exception ex)
        {
            string logMessage = $"Exception Details\n{GetExceptionDetails(ex)}";
            string filename = $"{DateTime.Now:dd-MM-yyyy--hh-mm-ss}.txt";
            string logFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }
            string filepath = Path.Combine(logFilePath, filename);
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(logMessage);
            }
        }

        private static string GetExceptionDetails(Exception ex)
        {
            List<string> details = new List<string>();
            Exception currentException = ex;
            //Execute the while loop until the inner exception is null
            while (currentException != null)
            {
                details.Add($"Message: {currentException.Message}");
                details.Add($"Source: {currentException.Source}");
                details.Add($"Stack Trace: {currentException.StackTrace}");
                details.Add("");

                currentException = currentException.InnerException;
            }
            return string.Join(Environment.NewLine, details);
        }
    }
}
