using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.EmailMarketing.Data
{
    /* This is used if database provider does't define
     * IEmailMarketingDbSchemaMigrator implementation.
     */
    public class NullEmailMarketingDbSchemaMigrator : IEmailMarketingDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}