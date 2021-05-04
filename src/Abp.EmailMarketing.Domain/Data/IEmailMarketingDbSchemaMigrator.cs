using System.Threading.Tasks;

namespace Abp.EmailMarketing.Data
{
    public interface IEmailMarketingDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
