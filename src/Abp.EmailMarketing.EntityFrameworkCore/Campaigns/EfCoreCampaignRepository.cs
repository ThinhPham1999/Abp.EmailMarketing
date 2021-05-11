using Abp.EmailMarketing.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Abp.EmailMarketing.Campaigns
{
    public class EfCoreCampaignRepository :
        EfCoreRepository<EmailMarketingDbContext, Campaign, Guid>, ICampaignRepository
    {
        public EfCoreCampaignRepository(IDbContextProvider<EmailMarketingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<Campaign> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(c => c.Name.Equals(name));
        }

        public async Task<List<Campaign>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    c => c.Name.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();

        }
    }
}
