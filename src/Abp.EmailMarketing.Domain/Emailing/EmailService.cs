using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.Security.Encryption;
using Volo.Abp.TextTemplating;

namespace Abp.EmailMarketing.Emailing
{
    public class EmailService : ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly ITemplateRenderer _templateRenderer;
        /*public IStringEncryptionService _encryptionService { get; set; }*/

        public EmailService(IEmailSender emailSender, ITemplateRenderer templateRenderer)
        {
            _emailSender = emailSender;
            _templateRenderer = templateRenderer;
        }

        public async Task SendAsync(string targetEmail, string content, string title)
        {
            var emailBody = await _templateRenderer.RenderAsync(
                StandardEmailTemplates.Message,
                new
                {
                    message = content
                }
            );

            await _emailSender.SendAsync(
                targetEmail,
                title,
                emailBody
            );
        }

        public async Task SendEmailAsync()
        {
            /*var encryptedGmailPassword = _encryptionService.Encrypt("your-gmail-password-here");*/
            await _emailSender.SendAsync("","recipient-email-here", "Email subject", "This is the email body...");
        }

    }
}
