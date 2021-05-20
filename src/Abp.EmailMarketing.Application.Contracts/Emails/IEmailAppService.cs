using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.EmailMarketing.Emails
{
    public interface IEmailAppService : IApplicationService
    {
        Task<EmailDto> GetAsync(Guid id);
        Task<PagedResultDto<EmailDto>> GetListAsync(GetEmailListDto input);
        Task<EmailDto> CreateAsync(CreateUpdateEmailDto input);
        Task UpdateAsync(Guid id, CreateUpdateEmailDto input);
        Task DeleteAsync(Guid id);
    }
}
