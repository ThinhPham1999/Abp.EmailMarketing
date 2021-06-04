using Abp.EmailMarketing.GroupContacts;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace Abp.EmailMarketing.Contacts
{
    [Authorize(EmailMarketingPermissions.Contacts.Default)]
    public class ContactAppService :
        /*CrudAppService<
            Contact, //The Contact entity
            ContactDto, //Used to show contacts
            Guid, //Primary key of the contact entity
            GetContactListDto, //Used for paging/sorting
            CreateUpdateContactDto>, //Used to create/update a contact*/
        EmailMarketingAppService, IContactAppService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ContactManager _contactManager;

        public ContactAppService(
            IGroupRepository groupRepository, IContactRepository contactRepository,
            ContactManager contactManager)
        {
            _groupRepository = groupRepository;
            _contactRepository = contactRepository;
            _contactManager = contactManager;
        }

        public async Task<ContactDto> GetAsync(Guid id)
        {
            var contact = await _contactRepository.GetAsync(id);
            return ObjectMapper.Map<Contact, ContactDto>(contact);
        }

        public async Task<PagedResultDto<ContactDto>> GetListAsync(GetContactListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Group.Name);
            }

            var contacts = await _contactRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _contactRepository.CountAsync()
                : await _contactRepository.CountAsync(contact => contact.Email.Contains(input.Filter));

            return new PagedResultDto<ContactDto>(
                totalCount,
                ObjectMapper.Map<List<Contact>, List<ContactDto>>(contacts)
            );
        }

        public async Task<ListResultDto<GroupLookupDto>> GetGroupLookupAsync()
        {
            var groups = await _groupRepository.GetListAsync();

            return new ListResultDto<GroupLookupDto>(
                ObjectMapper.Map<List<Group>, List<GroupLookupDto>>(groups)
            );
        }

        public async Task<ContactDto> CreateAsync(CreateUpdateContactDto input)
        {
            var group = await _groupRepository.GetListAsync();
            List<ContactGroup> contactGroups = new List<ContactGroup>();

            var contact = await _contactManager.CreateAsync(
                input.FirstName,
                input.LastName,
                input.Email,
                input.DateOfBirth,
                input.PhoneNumber,
                input.Addition,
                0,
                null,
                0
            );

            foreach (Guid id in input.GroupIds)
            {
                var item = group.FirstOrDefault(gid => gid.Id.ToString().Equals(id.ToString()));
                if (item != null)
                {
                    contactGroups.Add(new ContactGroup(
                            GuidGenerator.Create(),
                            contact.Id,
                            item.Id
                        ));
                }
            }

            contact.ContactGroups = contactGroups;

            await _contactRepository.InsertAsync(contact);
            //await _contactRepository.CreateContact(contact);

            return ObjectMapper.Map<Contact, ContactDto>(contact);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateContactDto input)
        {
            var contact = await _contactRepository.GetAsync(id, includeDetails: false);
            await _contactRepository.EnsureCollectionLoadedAsync(contact, x => x.ContactGroups);

            //List<Group> groups = await _groupRepository.GetListAsync();
            //Add new relationship
            foreach (Guid groupid in input.GroupIds)
            {
                var item = contact.ContactGroups.FirstOrDefault(gid => gid.GroupId.ToString().Equals(groupid.ToString()));
                if (item == null)
                {
                    contact.ContactGroups.Add(new ContactGroup(
                        GuidGenerator.Create(),
                        contact.Id,
                        groupid
                    ));
                }
            }

            //Remove relationship
            for (int i = 0; i < contact.ContactGroups.Count; i++)
            {
                Guid item = input.GroupIds.FirstOrDefault(gid => gid.ToString().Equals(contact.ContactGroups[i].GroupId.ToString()));
                if (item.ToString().Equals(Guid.Empty.ToString()))
                {
                    contact.ContactGroups.Remove(contact.ContactGroups[i]);
                    i--;
                }
            }
            //contact.Groups = new List<Group>();

            contact.Email = input.Email;
            //contact.ContactGroups = updateGroup;
            contact.DateOfBirth = input.DateOfBirth;
            contact.FirstName = input.FirstName;
            contact.LastName = input.LastName;
            contact.PhoneNumber = input.PhoneNumber;
            contact.Addition = input.Addition;
            
            await _contactRepository.UpdateAsync(contact);
            //await _contactRepository.UpdateContact(id, contact);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _contactRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<GroupLookupDto>> GetGroupByContactId(Guid id)
        {
            var contact = await _contactRepository.GetAsync(id, includeDetails: false);
            //order.Lines is empty on this stage

            await _contactRepository.EnsureCollectionLoadedAsync(contact, x => x.ContactGroups);
            var result = new List<Group>();
            foreach(var g in contact.ContactGroups)
            {
                result.Add(g.Group);
            }

            return new ListResultDto<GroupLookupDto>(
                ObjectMapper.Map<List<Group>, List<GroupLookupDto>>(result)
            );
        }
    }
}
