using Abp.EmailMarketing.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Abp.EmailMarketing.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(EmailMarketingEntityFrameworkCoreDbMigrationsModule),
        typeof(EmailMarketingApplicationContractsModule)
        )]
    public class EmailMarketingDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
