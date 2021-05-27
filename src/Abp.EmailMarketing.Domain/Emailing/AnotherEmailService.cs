using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MimeKit;
using MimeKit.Text;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.EmailMarketing.Emailing
{
    /*public interface IAnotherEmailService
    {
        void Send(string from, string to, string subject, string html);
    }*/

    public class AnotherEmailService : ITransientDependency
    {
        public ILogger<AnotherEmailService> Logger { get; set; }

        public AnotherEmailService()
        {
            Logger = NullLogger<AnotherEmailService>.Instance;
        }

        public void Send(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("vothithuqua11121997@gmail.com", "thuqua1997");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public async Task Send(string from, string to, string subject, string html, string username, string password)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task Send(EmailSetting emailSetting)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailSetting.DisplayName));
            email.To.Add(MailboxAddress.Parse(emailSetting.To));
            email.Subject = emailSetting.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailSetting.Body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(emailSetting.Host, emailSetting.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSetting.Mail, emailSetting.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            Logger.LogInformation("send mail to " + emailSetting.To);
        }

        public async Task SendEmailAsync(string displayName,string from, string to, string subject, string html, string username, string password)
        {
            await Send(new EmailSetting()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Mail = username,
                Password = password,
                To = to,
                Subject = subject,
                Body = html,
                DisplayName = displayName
            });
        }
    }
}
