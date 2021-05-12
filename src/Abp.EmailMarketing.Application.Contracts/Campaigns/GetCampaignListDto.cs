using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.EmailMarketing.Campaigns
{
    public class GetCampaignListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
