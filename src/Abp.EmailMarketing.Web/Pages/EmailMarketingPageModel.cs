using Abp.EmailMarketing.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.EmailMarketing.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class EmailMarketingPageModel : AbpPageModel
    {
        protected EmailMarketingPageModel()
        {
            LocalizationResourceType = typeof(EmailMarketingResource);
        }
    }
}