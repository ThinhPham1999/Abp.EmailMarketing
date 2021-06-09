using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.EmailMarketing.GroupContacts
{
    public class UpdateContactInGroupDto
    {
        public List<Guid> Id { get; set; }
        public Guid GroupId { get; set; }
    }
}
