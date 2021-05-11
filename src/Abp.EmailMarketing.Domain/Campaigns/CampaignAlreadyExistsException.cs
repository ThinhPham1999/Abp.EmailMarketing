using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp.EmailMarketing.Campaigns
{
    class CampaignAlreadyExistsException : BusinessException
    {
        public CampaignAlreadyExistsException(string name) :
            base(EmailMarketingDomainErrorCodes.CampaignAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
