using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.EmailMarketing.Emails
{
    public class EmailDto : EntityDto<Guid>
    {
        public string EmailString { get; set; }
        public string Password { get; set; }
        public int Order { get; set; }
    }
}
