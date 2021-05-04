using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Abp.EmailMarketing.EntityFrameworkCore
{
    public static class EmailMarketingDbContextModelCreatingExtensions
    {
        public static void ConfigureEmailMarketing(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(EmailMarketingConsts.DbTablePrefix + "YourEntities", EmailMarketingConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}