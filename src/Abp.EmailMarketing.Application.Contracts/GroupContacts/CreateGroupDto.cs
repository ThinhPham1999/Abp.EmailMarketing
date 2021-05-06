using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Abp.EmailMarketing.GroupContacts
{
    public class CreateGroupDto
    {
        [Required]
        [StringLength(GroupConsts.MaxNameLength)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
