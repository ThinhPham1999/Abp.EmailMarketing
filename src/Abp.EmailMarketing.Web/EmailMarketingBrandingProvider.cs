using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Abp.EmailMarketing.Web
{
    [Dependency(ReplaceServices = true)]
    public class EmailMarketingBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "EmailMarketing";
    }
}
