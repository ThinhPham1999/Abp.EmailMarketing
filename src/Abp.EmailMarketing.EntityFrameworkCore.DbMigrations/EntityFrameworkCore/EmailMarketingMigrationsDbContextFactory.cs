using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Abp.EmailMarketing.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class EmailMarketingMigrationsDbContextFactory : IDesignTimeDbContextFactory<EmailMarketingMigrationsDbContext>
    {
        public EmailMarketingMigrationsDbContext CreateDbContext(string[] args)
        {
            EmailMarketingEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<EmailMarketingMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new EmailMarketingMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Abp.EmailMarketing.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
