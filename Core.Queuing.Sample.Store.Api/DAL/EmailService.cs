using System.Net.Mail;
using System.Net;

namespace Core.Queuing.Sample.Store.Api.DAL
{
    public static class EmailService
    {
        public static void SendEmail(string toAddress, string subject, string body)
        {
            // Gmail SMTP settings
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("omaraljoundi@gmail.com", "0530148594Oo*"),
                EnableSsl = true,
            };

            // Email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress("omaraljoundi@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(toAddress);

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
