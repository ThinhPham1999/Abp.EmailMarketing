using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp.EmailMarketing.Emails
{
    class EmailAlreadyExistsException : BusinessException
    {
        public EmailAlreadyExistsException(string emailString) :
            base(EmailMarketingDomainErrorCodes.CampaignAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
