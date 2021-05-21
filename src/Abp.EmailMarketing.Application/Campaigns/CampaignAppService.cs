using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.Emailing;
using Abp.EmailMarketing.Emails;
using Abp.EmailMarketing.GroupContacts;
using Abp.EmailMarketing.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Abp.EmailMarketing.Campaigns
{
    [Authorize(EmailMarketingPermissions.Campaign.Default)]
    public class CampaignAppService :
         EmailMarketingAppService, ICampaignAppService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly CampaignManager _campaignManager;
        private readonly IGroupRepository _groupRepository;
        private readonly EmailService _emailService;
        private readonly IContactRepository _contactRepository;
        private readonly IEmailRepository _emailRepository;

        public CampaignAppService(ICampaignRepository campaignRepository, CampaignManager campaignManager,
            IGroupRepository groupRepository, EmailService emailService,
            IContactRepository contactRepository, IEmailRepository emailRepository)
        {
            _campaignRepository = campaignRepository;
            _campaignManager = campaignManager;
            _groupRepository = groupRepository;
            _emailService = emailService;
            _contactRepository = contactRepository;
            _emailRepository = emailRepository;
        }

        public async Task<CampaignDto> CreateAsync(CreateUpdateCampaignDto input)
        {
            List<Group> groups = new List<Group>();
            //var group = _groupRepository.GetAsync();
            if (input.GroupId.Count > 0)
            {
                foreach(Guid groupDto in input.GroupId)
                {
                    var group = await _groupRepository.GetAsync(groupDto);
                    groups.Add(group);
                }
            }
            var campaign = await _campaignManager.CreateAsync(
               input.Name,
               input.Description,
               input.Schedule,
               input.Title,
               input.Content,
               groups
           );

            await _campaignRepository.InsertAsync(campaign);
            var emails = await _emailRepository.GetListAsync();
            
            
            foreach(Group group in campaign.Groups)
            {
                var contacts = await _contactRepository.GetListAsync();
                contacts = contacts.Where(c => c.GroupId.Equals(group.Id)).ToList();
                foreach (Contact c in contacts)
                {
                    var email = emails.OrderBy(e => e.Order).FirstOrDefault();
                    AnotherEmailService service = new AnotherEmailService();
                    //await _emailService.SendEmailAsync(email.EmailString, c.Email, input.Content, input.Title);
                    service.Send("abp@gmail.com", c.Email, input.Title, input.Content, email.EmailString, email.Password);
                    email.Order++;
                }
            }
            await _emailRepository.UpdateManyAsync(emails);

            return ObjectMapper.Map<Campaign, CampaignDto>(campaign);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CampaignDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDto<CampaignDto>> GetListAsync(GetCampaignListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Group.Name);
            }

            var campaigns = await _campaignRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _campaignRepository.CountAsync()
                : await _campaignRepository.CountAsync(group => group.Name.Contains(input.Filter));

            return new PagedResultDto<CampaignDto>(
                totalCount,
                ObjectMapper.Map<List<Campaign>, List<CampaignDto>>(campaigns)
            );
        }

        Task ICampaignAppService.UpdateAsync(Guid id, CreateUpdateCampaignDto input)
        {
            throw new NotImplementedException();
        }
    }
}
