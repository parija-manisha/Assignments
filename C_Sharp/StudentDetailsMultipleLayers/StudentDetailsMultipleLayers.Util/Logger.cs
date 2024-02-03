using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Util
{
    public class Logger
    {
        public static void AddError(String text, Exception ex) { 
            string logMessage = $"Exception Details\n{ex.Message}";

            string filename = $"{DateTime.Now:dd-MM-yyyy--hh-mm-ss}.txt";
            string filepath = Path.Combine(ConfigurationManager.AppSettings["LogFilePath"], filename);

            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(logMessage);
            }
        }
    }
}
