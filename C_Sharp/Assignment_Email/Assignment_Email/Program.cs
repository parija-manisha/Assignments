using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main()
    {
        string smtpServer = "sandbox.smtp.mailtrap.io";
        int smtpPort = 2525;
        string smtpUsername = "manishaparija26@gmail.com"; 
        string smtpPassword = "12Manisha"; 

        string fromEmail = "manishaparija26@gmail.com";

        Console.WriteLine("Enter recipient names (comma-separated):");
        string recipientNamesInput = Console.ReadLine();

        string[] recipientNames = recipientNamesInput.Split(',');

        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(fromEmail);

        foreach (string name in recipientNames)
        {
            string toEmail = $"{name.Trim().ToLower()}@gmail.com";
            mailMessage.To.Add(toEmail);
        }

        mailMessage.Subject = "Test Email";
        mailMessage.Body = "This is a test email sent from C# to multiple recipients.";

        SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
        smtpClient.EnableSsl = true; 

        try
        {
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }

        Console.ReadLine();
    }
}
