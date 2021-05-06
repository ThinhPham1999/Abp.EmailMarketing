using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.EmailMarketing.Campaigns
{
    public class Campaign : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Content { get; set;}
        public DateTime Schedule { get; set; }


       
    }
}
