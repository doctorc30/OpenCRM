using System.Net.Mail;

namespace bbom.Admin.Core.Services.EmailService
{
    public class ServerEmailService : IEmailService
    {
        public void SendMail(string emailTo, string title, string body, bool isHtmlBody)
        {
            // наш email с заголовком письма
            MailAddress from = new MailAddress("", title);
            // кому отправляем
            MailAddress to = new MailAddress(emailTo);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to)
            {
                Subject = title,
                Body = body,
                IsBodyHtml = isHtmlBody
            };
            // тема письма
            // текст письма - включаем в него ссылку
            // адрес smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587)
            {
                Credentials = new System.Net.NetworkCredential("", ""),
                EnableSsl = true
            };
            // логин и пароль
            smtp.Send(m);
        }

        public void SendMail(MailMessage message)
        {
            // наш email с заголовком письма
            MailAddress from = new MailAddress("");

            message.From = from;
            // тема письма
            // текст письма - включаем в него ссылку
            // адрес smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587)
            {
                Credentials = new System.Net.NetworkCredential("", ""),
                EnableSsl = true
            };
            // логин и пароль
            smtp.Send(message);
        }
    }
}