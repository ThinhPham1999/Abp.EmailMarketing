using Abp.EmailMarketing.GroupContacts;
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
    public class CampaignAppService :
         EmailMarketingAppService, ICampaignAppService
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignAppService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public Task<CampaignDto> CreateAsync(CreateUpdateCampaignDto input)
        {
            throw new NotImplementedException();
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
