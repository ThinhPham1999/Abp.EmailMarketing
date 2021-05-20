using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Abp.EmailMarketing.Emails
{
    public class CreateUpdateEmailDto
    {
        [Required]
        [StringLength(EmailConsts.MaxEmailStringLength)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string EmailString { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
