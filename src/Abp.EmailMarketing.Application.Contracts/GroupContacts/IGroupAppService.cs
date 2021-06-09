using Abp.EmailMarketing.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.EmailMarketing.GroupContacts
{
    public interface IGroupAppService : IApplicationService
    {
        Task<GroupDto> GetAsync(Guid id);
        Task<PagedResultDto<GroupDto>> GetListAsync(GetGroupListDto input);
        Task<GroupDto> CreateAsync(CreateGroupDto input);
        Task UpdateAsync(Guid id, UpdateGroupDto input);
        Task DeleteAsync(Guid id);
        Task<List<ContactDto>> GetListContact();
        Task<List<ContactDto>> GetListContactByGroup(Guid groupId);

        Task UpdateContactInGroup(UpdateContactInGroupDto input);
    }
}
