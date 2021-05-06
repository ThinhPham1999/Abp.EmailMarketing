using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.EmailMarketing.GroupContacts
{
    public class GetGroupListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
