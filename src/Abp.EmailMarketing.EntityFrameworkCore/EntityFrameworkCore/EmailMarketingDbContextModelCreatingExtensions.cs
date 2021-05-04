using Abp.EmailMarketing.Contacts;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.EmailMarketing.EntityFrameworkCore
{
    public static class EmailMarketingDbContextModelCreatingExtensions
    {
        public static void ConfigureEmailMarketing(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            builder.Entity<Contact>(c =>
            {
                c.ToTable(EmailMarketingConsts.DbTablePrefix + "Contact", EmailMarketingConsts.DbSchema);
                c.ConfigureByConvention();
                c.Property(x => x.Email).IsRequired().HasColumnType("nvarchar(max)");
                c.Property(x => x.FirstName).IsRequired().HasColumnType("nvarchar(50)");
                c.Property(x => x.LastName).IsRequired().HasColumnType("nvarchar(50)");
                c.Property(x => x.PhoneNumber).HasColumnType("nvarchar(50)");
                c.Property(x => x.DateOfBirth).HasColumnType("datetime2");
                c.Property(x => x.Type).IsRequired();
            });
        }
    }
}