using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Abp.EmailMarketing.Campaigns
{
    public class CreateUpdateCampaignDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; private set; }
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Schedule { get; set; } = DateTime.Now;

        public List<Guid> GroupId { get; set; }
    }
}
