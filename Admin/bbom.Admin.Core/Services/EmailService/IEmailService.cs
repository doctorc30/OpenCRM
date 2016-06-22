using System.Net.Mail;

namespace bbom.Admin.Core.Services.EmailService
{
    public interface IEmailService
    {
        void SendMail(string emailTo, string title, string body, bool isHtmlBody);
        void SendMail(MailMessage message);
    }
}