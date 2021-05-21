using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Abp.EmailMarketing.Emails
{
    public class EmailManager : DomainService
    {
        private readonly IEmailRepository _emailRepository;

        public EmailManager(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<Email> CreateAsync(
            [NotNull] string emailString,
            [NotNull] string password,
            int order)
        {
            Check.NotNullOrWhiteSpace(emailString, nameof(emailString));
            var existingEmail = await _emailRepository.FindByEmailStringAsync(emailString);
            if (existingEmail != null)
            {
                throw new EmailAlreadyExistsException(emailString);
            }

            return new Email(
                GuidGenerator.Create(),
                emailString,
                password,
                order
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Email email,
            [NotNull] string newEmailString)
        {
            Check.NotNull(email, nameof(email));
            Check.NotNullOrWhiteSpace(newEmailString, nameof(newEmailString));

            var existingGroup = await _emailRepository.FindByEmailStringAsync(newEmailString);
            if (existingGroup != null)
            {
                throw new EmailAlreadyExistsException(newEmailString);
            }

            email.ChangeEmailString(newEmailString);
        }
    }
}
