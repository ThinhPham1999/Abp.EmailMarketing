using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.EmailMarketing.Emails
{
    public class GetEmailListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
