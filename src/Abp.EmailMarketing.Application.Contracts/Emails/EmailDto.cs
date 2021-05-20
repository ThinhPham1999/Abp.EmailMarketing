using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.EmailMarketing.Emails
{
    public class EmailDto : EntityDto<Guid>
    {
        public string EmailString { get; private set; }
        public string Password { get; set; }
    }
}
