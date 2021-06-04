using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.EmailMarketing.Contacts
{
    public interface IContactRepository : IRepository<Contact, Guid>
    {
        Task CreateContact(Contact contact);
        Task<Contact> FindByEmailAsync(string email);

        Task<List<Contact>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );

        Task UpdateContact(Guid id, Contact contact);
    }
}
