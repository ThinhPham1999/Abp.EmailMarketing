using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.EmailMarketing.Campaigns
{
    public interface ICampaignRepository : IRepository<Campaign, Guid>
    {
        Task<Campaign> FindByNameAsync(string name);

        Task<List<Campaign>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
