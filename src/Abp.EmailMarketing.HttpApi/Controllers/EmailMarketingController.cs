using Abp.EmailMarketing.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.EmailMarketing.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class EmailMarketingController : AbpController
    {
        protected EmailMarketingController()
        {
            LocalizationResource = typeof(EmailMarketingResource);
        }
    }
}