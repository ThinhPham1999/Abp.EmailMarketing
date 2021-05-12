using Abp.EmailMarketing.GroupContacts;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.EmailMarketing.Campaigns
{
    public class CampaignDto : EntityDto<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Schedule { get; set; }

        public List<GroupDto> GroupId { get; set; }
    }
}
