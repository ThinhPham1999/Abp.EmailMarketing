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
        public Guid GroupId { get; set; }

        public Contact()
        {

        }
    }
}
