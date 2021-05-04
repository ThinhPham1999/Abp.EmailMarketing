using Volo.Abp.Modularity;

namespace Abp.EmailMarketing
{
    [DependsOn(
        typeof(EmailMarketingApplicationModule),
        typeof(EmailMarketingDomainTestModule)
        )]
    public class EmailMarketingApplicationTestModule : AbpModule
    {

    }
}