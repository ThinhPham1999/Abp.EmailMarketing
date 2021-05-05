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
            Contact,      //The Contact entity
            ContactDto,   //Used to show contact
            Guid,         //Primary key of contact
            PagedAndSortedResultRequestDto, //Used to paging/sorting
            CreateUpdateContactDto>,        //Used to create/update contact
        IContactAppService //implement the IContactAppService
    {
        public ContactAppService(IRepository<Contact, Guid> repository) : base(repository)
        {

        }
    }
}
