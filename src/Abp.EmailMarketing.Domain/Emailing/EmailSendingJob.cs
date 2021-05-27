using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Abp.EmailMarketing.Emailing
{
    public class EmailSendingJob :
        AsyncBackgroundJob<EmailSetting>, ITransientDependency
    {
        //public ILogger<EmailSendingJob> Logger { get; set; }
        private readonly AnotherEmailService _anotherEmailService;

        public EmailSendingJob(AnotherEmailService anotherEmailService)
        {
            Logger = NullLogger<EmailSendingJob>.Instance;
            _anotherEmailService = anotherEmailService;
        }

        public override async Task ExecuteAsync(EmailSetting args)
        {
            await _anotherEmailService.Send(args);
        }
    }
}
