using WEBAPI.Utility;

namespace WEBAPI.Contracts
{
    public interface IEmailService
    {
        void SendEmailAsync();

        EmailService SetEmail(string email);
        EmailService SetSubject(string subject);
        EmailService SetBody(string body);


    }
}
