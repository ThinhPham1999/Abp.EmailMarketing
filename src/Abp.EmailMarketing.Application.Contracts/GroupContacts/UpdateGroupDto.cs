using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Abp.EmailMarketing.GroupContacts
{
    public class UpdateGroupDto
    {
        [Required]
        [StringLength(GroupConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
