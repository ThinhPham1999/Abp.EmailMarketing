using System;
using System.Collections.Generic;
using System.Text;
using Abp.EmailMarketing.Localization;
using Volo.Abp.Application.Services;

namespace Abp.EmailMarketing
{
    /* Inherit your application services from this class.
     */
    public abstract class EmailMarketingAppService : ApplicationService
    {
        protected EmailMarketingAppService()
        {
            LocalizationResource = typeof(EmailMarketingResource);
        }
    }
}
