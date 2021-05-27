using Abp.EmailMarketing.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Abp.EmailMarketing
{
    [DependsOn(
        typeof(EmailMarketingDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(EmailMarketingApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpBackgroundJobsModule)
        )]
    public class EmailMarketingApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EmailMarketingApplicationModule>();
            });

            /*Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false; //Disables job execution
            });*/

            Configure<AbpBackgroundJobWorkerOptions>(options =>
            {
                options.DefaultTimeout = 864000; //10 days (as seconds)
            });
        }
    }
}
