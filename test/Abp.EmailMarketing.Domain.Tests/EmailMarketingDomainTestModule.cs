using Abp.EmailMarketing.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.EmailMarketing
{
    [DependsOn(
        typeof(EmailMarketingEntityFrameworkCoreTestModule)
        )]
    public class EmailMarketingDomainTestModule : AbpModule
    {

    }
}