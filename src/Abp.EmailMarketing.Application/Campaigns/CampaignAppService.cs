using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.Emailing;
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

        public CampaignAppService(ICampaignRepository campaignRepository, CampaignManager campaignManager,
            IGroupRepository groupRepository, EmailService emailService,
            IContactRepository contactRepository)
        {
            _campaignRepository = campaignRepository;
            _campaignManager = campaignManager;
            _groupRepository = groupRepository;
            _emailService = emailService;
            _contactRepository = contactRepository;
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
            foreach(Group group in campaign.Groups)
            {
                var contacts = _contactRepository.GetListAsync().Result;
                foreach(Contact c in contacts)
                {
                    await _emailService.SendAsync(c.Email, input.Content, input.Title);
                }
            }

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
