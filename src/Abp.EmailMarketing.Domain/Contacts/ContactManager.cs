using Abp.EmailMarketing.GroupContacts;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Abp.EmailMarketing.Contacts
{
    public class ContactManager : DomainService
    {
        private readonly IContactRepository _contactRepository;

        public ContactManager(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> CreateAsync(
            [NotNull] string firstName,
            [NotNull] string lastName,
            [NotNull] string email,
            DateTime? dateOfBirth,
            string phoneNumber,
            string addition,
            int type,
            IList<ContactGroup> contactGroups,
            int status)
        {
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.NotNullOrWhiteSpace(lastName, nameof(lastName));
            Check.NotNullOrWhiteSpace(email, nameof(email));
            var existingContact = await _contactRepository.FindByEmailAsync(email);
            if (existingContact != null)
            {
                throw new ContactAlreadyExistsException(email);
            }

            return new Contact(
                GuidGenerator.Create(),
                firstName,
                lastName,
                email,
                dateOfBirth,
                phoneNumber,
                addition,
                type,
                contactGroups,
                status
            );
        }
    }
}
