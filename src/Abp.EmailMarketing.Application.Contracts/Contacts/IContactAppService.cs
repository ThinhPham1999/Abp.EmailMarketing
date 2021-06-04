using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.EmailMarketing.Contacts
{
    public interface IContactAppService : IApplicationService
    /*ICrudAppService<   //Define Crud method
        ContactDto,    //Used to show contact
        Guid,          //Primary key of contact
        GetContactListDto, // Used to paging/sorting
        CreateUpdateContactDto>         // Used to create/update a contact*/
    {
        Task<ListResultDto<GroupLookupDto>> GetGroupLookupAsync();
        Task<ContactDto> GetAsync(Guid id);
        Task<PagedResultDto<ContactDto>> GetListAsync(GetContactListDto input);
        Task<ContactDto> CreateAsync(CreateUpdateContactDto input);
        Task UpdateAsync(Guid id, CreateUpdateContactDto input);
        Task DeleteAsync(Guid id);
        Task<ListResultDto<GroupLookupDto>> GetGroupByContactId(Guid id);
    }
}
