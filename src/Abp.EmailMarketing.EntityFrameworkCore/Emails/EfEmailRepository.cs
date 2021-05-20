using Abp.EmailMarketing.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Abp.EmailMarketing.Emails
{
    public class EfEmailRepository :
        EfCoreRepository<EmailMarketingDbContext, Email, Guid>, IEmailRepository
    {
        public EfEmailRepository(IDbContextProvider<EmailMarketingDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public async Task<Email> FindByEmailStringAsync(string emailString)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(c => c.EmailString.Equals(emailString));
        }

        public async Task<List<Email>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    c => c.EmailString.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
