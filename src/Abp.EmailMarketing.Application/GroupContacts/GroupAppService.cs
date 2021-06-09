using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Abp.EmailMarketing.GroupContacts
{
    [Authorize(EmailMarketingPermissions.Groups.Default)]
    public class GroupAppService : EmailMarketingAppService, IGroupAppService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly GroupManager _groupManager;
        private readonly IContactRepository _contactRepository;

        public GroupAppService(IGroupRepository groupRepository, GroupManager groupManager,
            IContactRepository contactRepository)
        {
            _groupRepository = groupRepository;
            _groupManager = groupManager;
            _contactRepository = contactRepository;
        }

        [Authorize(EmailMarketingPermissions.Groups.Create)]
        public async Task<GroupDto> CreateAsync(CreateGroupDto input)
        {
            var group = await _groupManager.CreateAsync(
                input.Name,
                input.Description
            );

            await _groupRepository.InsertAsync(group);

            return ObjectMapper.Map<Group, GroupDto>(group);
        }

        [Authorize(EmailMarketingPermissions.Groups.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _groupRepository.DeleteAsync(id);
        }

        public async Task<GroupDto> GetAsync(Guid id)
        {
            var group = await _groupRepository.GetAsync(id);
            return ObjectMapper.Map<Group, GroupDto>(group);
        }

        public async Task<PagedResultDto<GroupDto>> GetListAsync(GetGroupListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Group.Name);
            }

            var groups = await _groupRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _groupRepository.CountAsync()
                : await _groupRepository.CountAsync(group => group.Name.Contains(input.Filter));

            return new PagedResultDto<GroupDto>(
                totalCount,
                ObjectMapper.Map<List<Group>, List<GroupDto>>(groups)
            );
        }

        [Authorize(EmailMarketingPermissions.Groups.Edit)]
        public async Task UpdateAsync(Guid id, UpdateGroupDto input)
        {
            var group = await _groupRepository.GetAsync(id);

            if (group.Name != input.Name)
            {
                await _groupManager.ChangeNameAsync(group, input.Name);
            }

            group.Description = input.Description;

            await _groupRepository.UpdateAsync(group);
        }


        public async Task<List<ContactDto>> GetListContact()
        {
            var list = await _contactRepository.GetListAsync();
            var result = ObjectMapper.Map<List<Contact>, List<ContactDto>>(list);
            return result;
        }

        public async Task<List<ContactDto>> GetListContactByGroup(Guid groupId)
        {
            var list = await _contactRepository.GetListAsync();
            foreach (var contact in list)
            {
                await _contactRepository.EnsureCollectionLoadedAsync(contact, x => x.ContactGroups);
            }

            list = list.Where(c => c.ContactGroups.Any(gid => gid.GroupId.ToString().Equals(groupId.ToString()))).ToList();
            return ObjectMapper.Map<List<Contact>, List<ContactDto>>(list);
        }

        public async Task UpdateContactInGroup(UpdateContactInGroupDto input)
        {
            var group = await _groupRepository.GetAsync(input.GroupId, includeDetails: false);
            await _groupRepository.EnsureCollectionLoadedAsync(group, x => x.ContactGroups);

            //Add new relationship
            foreach (Guid contactId in input.Id)
            {
                var item = group.ContactGroups.FirstOrDefault(gid => gid.ContactId.ToString().Equals(contactId.ToString()));
                if (item == null)
                {
                    group.ContactGroups.Add(new ContactGroup(
                        GuidGenerator.Create(),
                        contactId,
                        input.GroupId
                    ));
                }
            }

            //Remove relationship
            for (int i = 0; i < group.ContactGroups.Count; i++)
            {
                Guid item = input.Id.FirstOrDefault(cid => cid.ToString().Equals(group.ContactGroups[i].ContactId.ToString()));
                if (item.ToString().Equals(Guid.Empty.ToString()))
                {
                    group.ContactGroups.Remove(group.ContactGroups[i]);
                    i--;
                }
            }

            await _groupRepository.UpdateAsync(group);
        } 
    }
}
