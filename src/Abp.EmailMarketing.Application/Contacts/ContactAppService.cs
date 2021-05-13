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
        CrudAppService<
            Contact, //The Contact entity
            ContactDto, //Used to show contacts
            Guid, //Primary key of the contact entity
            GetContactListDto, //Used for paging/sorting
            CreateUpdateContactDto>, //Used to create/update a contact
        IContactAppService //implement the IContactAppService
    {
        private readonly IGroupRepository _groupRepository;

        public ContactAppService(
            IRepository<Contact, Guid> repository,
            IGroupRepository groupRepository)
            : base(repository)
        {
            _groupRepository = groupRepository;
            GetPolicyName = EmailMarketingPermissions.Contacts.Default;
            GetListPolicyName = EmailMarketingPermissions.Contacts.Default;
            CreatePolicyName = EmailMarketingPermissions.Contacts.Create;
            UpdatePolicyName = EmailMarketingPermissions.Contacts.Edit;
            DeletePolicyName = EmailMarketingPermissions.Contacts.Create;
        }

        public override async Task<ContactDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Contact> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join contacts and groups
            var query = from contact in queryable
                        join groups in _groupRepository on contact.GroupId equals groups.Id
                        where contact.Id == id
                        select new { contact, groups };

            //Execute the query and get the contact with group
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Contact), id);
            }

            var contactDto = ObjectMapper.Map<Contact, ContactDto>(queryResult.contact);
            contactDto.GroupName = queryResult.groups.Name;
            return contactDto;
        }

        public override async Task<PagedResultDto<ContactDto>> GetListAsync(GetContactListDto input)
        {
            //Get the IQueryable<Contact> from the repository
            var queryable = await Repository.GetQueryableAsync();

            var query = from contact in queryable
                        join groups in _groupRepository on contact.GroupId equals groups.Id
                        //where contact.Email == input.Filter
                        select new { contact, groups };
            //Prepare a query to join contacts and groups
            if (!input.Filter.IsNullOrWhiteSpace())
            {
                query = from contact in queryable
                        join groups in _groupRepository on contact.GroupId equals groups.Id
                        where contact.Email.ToUpper().Contains(input.Filter.ToUpper())
                        select new { contact, groups };
            }

            //Get the total count with another query
            var totalCount = input.Filter == null ?
                await Repository.GetCountAsync() :
                AsyncExecuter.ToListAsync(query).Result.Count;

            //Paging
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
                

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of ContactDto objects
            var contactDtos = queryResult.Select(x =>
            {
                var contactDto = ObjectMapper.Map<Contact, ContactDto>(x.contact);
                contactDto.GroupName = x.groups.Name;
                return contactDto;
            }).ToList();


            return new PagedResultDto<ContactDto>(
                totalCount,
                contactDtos
            );
        }

        public async Task<ListResultDto<GroupLookupDto>> GetGroupLookupAsync()
        {
            var groups = await _groupRepository.GetListAsync();

            return new ListResultDto<GroupLookupDto>(
                ObjectMapper.Map<List<Group>, List<GroupLookupDto>>(groups)
            );
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"contact.{nameof(Contact.LastName)}";
            }

            if (sorting.Contains("groupName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "groupName",
                    "groups.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"contact.{sorting}";
        }
    }
}
