using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.EmailMarketing.Contacts
{
    public class GroupLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
