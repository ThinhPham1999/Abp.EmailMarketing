using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abp.EmailMarketing.Data;
using Volo.Abp.DependencyInjection;

namespace Abp.EmailMarketing.EntityFrameworkCore
{
    public class EntityFrameworkCoreEmailMarketingDbSchemaMigrator
        : IEmailMarketingDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreEmailMarketingDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the EmailMarketingMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<EmailMarketingMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}