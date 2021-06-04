using Abp.EmailMarketing.GroupContacts;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.EmailMarketing.Contacts
{
    public class Contact : AuditedAggregateRoot<Guid>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Addition { get; set; }
        public int Status { get; set; }
        public ContactType Type { get; set; }
        public virtual IList<ContactGroup> ContactGroups { get; set; }

        public Contact()
        {

        }

        internal Contact(
            Guid id,
            [NotNull] string firstName,
            [NotNull] string lastName,
            [NotNull] string email,
            DateTime? dateOfBirth,
            string phoneNumber,
            string addition,
            int type,
            IList<ContactGroup> groups,
            int status
            )
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Addition = addition;
            ContactGroups = groups;
            Status = status;
            Email = email;
            Type = ContactType.Group01;
        }
    }
}
