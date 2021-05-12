using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Abp.EmailMarketing.Campaigns
{
    public interface ICampaignAppService : IApplicationService
    {
        Task<CampaignDto> GetAsync(Guid id);
        Task<PagedResultDto<CampaignDto>> GetListAsync(GetCampaignListDto input);
        Task<CampaignDto> CreateAsync(CreateUpdateCampaignDto input);
        Task UpdateAsync(Guid id, CreateUpdateCampaignDto input);
        Task DeleteAsync(Guid id);
    }
}
