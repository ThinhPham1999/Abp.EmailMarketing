using Abp.EmailMarketing.Campaigns;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.GroupContacts;
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

                //Add relation
                c.HasOne<Group>().WithMany().HasForeignKey(x => x.GroupId).IsRequired();
            });

            builder.Entity<Group>(g =>
            {
                g.ToTable(EmailMarketingConsts.DbTablePrefix + "Group", EmailMarketingConsts.DbSchema);
                g.ConfigureByConvention();
                g.Property(x => x.Name).IsRequired().HasMaxLength(GroupConsts.MaxNameLength);
                g.HasIndex(x => x.Name);
            });

            builder.Entity<Campaign>(c =>
            {
                c.ToTable(EmailMarketingConsts.DbTablePrefix + "Campaign", EmailMarketingConsts.DbSchema);
                c.ConfigureByConvention();
                c.Property(x => x.Name).IsRequired().HasMaxLength(100);
                c.Property(x => x.Content).HasColumnType("nvarchar(max)");
                c.Property(x => x.Title).HasColumnType("nvarchar(100)");
                c.Property(x => x.Schedule).HasColumnType("datetime2");
            });
        }
    }
}