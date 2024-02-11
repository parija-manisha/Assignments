using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DemoUserManagement.Business
{
    public class Email
    {
        public static void SendEmail(string recipientEmail)
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(ConfigurationManager.AppSettings["fromEmail"]);
                    message.To.Add(new MailAddress(recipientEmail));

                    message.Subject = "Registration Successful";
                    message.Body = $"Congratulations! You have registered successfully. All the best!";

                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.Host = ConfigurationManager.AppSettings["smtpServer"];
                        int.TryParse(ConfigurationManager.AppSettings["smtpPort"], out int smtpPort);
                        smtpClient.Port = smtpPort;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(
                            ConfigurationManager.AppSettings["smtpUsername"],
                            ConfigurationManager.AppSettings["smtpPassword"]
                        );
                        smtpClient.EnableSsl = true;

                        smtpClient.Send(message);
                    }
                }

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                // Log or handle the exception as needed
            }
        }
    }
}
