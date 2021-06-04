using Abp.EmailMarketing.GroupContacts;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.EmailMarketing.Contacts
{
    public class ContactDto : AuditedEntityDto<Guid>
    {
        public List<Guid> GroupIds { get; set; }
        public string GroupName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Addition { get; set; }
        public int Status { get; set; }
        public ContactType Type { get; set; }

        
    }
}
