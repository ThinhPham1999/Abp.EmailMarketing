using Abp.EmailMarketing.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.EmailMarketing.Contacts
{
    public class EfContactRepository : 
        EfCoreRepository<EmailMarketingDbContext, Contact, Guid>, IContactRepository
    {
        public EfContactRepository(IDbContextProvider<EmailMarketingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
