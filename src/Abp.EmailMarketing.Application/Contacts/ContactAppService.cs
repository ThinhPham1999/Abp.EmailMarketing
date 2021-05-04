using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Abp.EmailMarketing.Contacts
{
    public class ContactAppService :
        CrudAppService<
            Contact,
            ContactDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateContactDto,
            IContactAppService>
    {
        public ContactAppService(IRepository<Contact, Guid> repository) : base(repository)
        {

        }
    }
}
