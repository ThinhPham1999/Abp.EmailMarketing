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

namespace Abp.EmailMarketing.GroupContacts
{
    public class EfCoreGroupRepository
        : EfCoreRepository<EmailMarketingDbContext, Group, Guid>, IGroupRepository
    {
        public EfCoreGroupRepository(IDbContextProvider<EmailMarketingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<Group> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(group => group.Name.Equals(name));
        }

        public async Task<List<Group>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    group => group.Name.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();

        }
    }
}
