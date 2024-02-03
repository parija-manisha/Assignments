using System;
using System.Configuration;
using System.IO;

namespace ProjectStudentDetailsMultipleLayer.Util
{
    public class Logger
    {
        public static void AddError(string message, Exception exception)
        {
            string logMessage = $"{DateTime.Now}: {message}\nException details: {exception.InnerException}";
            Console.WriteLine(logMessage);

            LogToFile(logMessage);
        }

        private static void LogToFile(string logMessage)
        {
            try
            {
                string fileName = $"{DateTime.Now:yyyy/MM/dd_HH-mm-ss}.txt";
                string filePath = Path.Combine(ConfigurationManager.AppSettings["LogFilePath"], fileName);

                Directory.CreateDirectory(ConfigurationManager.AppSettings["LogFilePath"]);

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while writing to log file: {ex}");
            }
        }
    }
}
