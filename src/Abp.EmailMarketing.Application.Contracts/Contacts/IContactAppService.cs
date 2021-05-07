using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.EmailMarketing.Contacts
{
    public interface IContactAppService :
        ICrudAppService<   //Define Crud method
            ContactDto,    //Used to show contact
            Guid,          //Primary key of contact
            PagedAndSortedResultRequestDto, // Used to paging/sorting
            CreateUpdateContactDto>         // Used to create/update a contact
    {
        Task<ListResultDto<GroupLookupDto>> GetGroupLookupAsync();
    }
}
