using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.EmailMarketing.Emails
{
    public interface IEmailRepository : IRepository<Email, Guid>
    {
        Task<Email> FindByEmailStringAsync(string emailString);

        Task<List<Email>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
