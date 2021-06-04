using Abp.EmailMarketing.GroupContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.EmailMarketing.Contacts
{
    public class ContactGroup : AuditedAggregateRoot<Guid>
    {
        public Guid ContactId { get; set; }
        public Guid GroupId { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Group Group { get; set; }

        public ContactGroup(Guid id,Guid contactId, Guid groupId)
        {
            Id = id;
            ContactId = contactId;
            GroupId = groupId;
        }
    }
}
