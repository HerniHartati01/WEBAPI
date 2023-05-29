using Azure.Core;
using System.Net.Mail;
using WEBAPI.Contracts;

namespace WEBAPI.Utility
{
    public class FluentEmail
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string HtmlMessage { get; set; }
    }
    public class EmailService : IEmailService
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string fromEmailAddress;

        private FluentEmail fluentEmail = new();

        public EmailService(string smtpServer, int smtpPort, string fromEmailAddress)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.fromEmailAddress = fromEmailAddress;
        }

        public EmailService SetBody(string body)
        {
            fluentEmail.HtmlMessage = body;
            return this;
        }

        public EmailService SetEmail(string email)
        {
            fluentEmail.Email = email;
            return this;
        }

        public EmailService SetSubject(string subject)
        {
            fluentEmail.Subject = subject;
            return this;
        }

        public void SendEmailAsync()
        {
            var message = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = fluentEmail.Subject,
                Body = fluentEmail.HtmlMessage,
                To = { fluentEmail.Email },
                IsBodyHtml = true
            };

            using var client = new SmtpClient(smtpServer, smtpPort);
            client.Send(message);

            message.Dispose();
            client.Dispose();
        }
    }
}
