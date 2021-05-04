using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Abp.EmailMarketing.EntityFrameworkCore
{
    [DependsOn(
        typeof(EmailMarketingEntityFrameworkCoreModule)
        )]
    public class EmailMarketingEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EmailMarketingMigrationsDbContext>();
        }
    }
}
