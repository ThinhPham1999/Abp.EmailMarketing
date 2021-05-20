using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Abp.EmailMarketing.Emails
{
    public class EmailAppService : EmailMarketingAppService, IEmailAppService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly EmailManager _emailManager;

        public EmailAppService(IEmailRepository emailRepository, EmailManager emailManager)
        {
            _emailRepository = emailRepository;
            _emailManager = emailManager;
        }

        public async Task<EmailDto> CreateAsync(CreateUpdateEmailDto input)
        {
            var email = await _emailManager.CreateAsync(
                 input.EmailString,
                 input.Password
             );

            await _emailRepository.InsertAsync(email);

            return ObjectMapper.Map<Email, EmailDto>(email);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _emailRepository.DeleteAsync(id);
        }

        public async Task<EmailDto> GetAsync(Guid id)
        {
            var email = await _emailRepository.GetAsync(id);
            return ObjectMapper.Map<Email, EmailDto>(email);
        }

        public async Task<PagedResultDto<EmailDto>> GetListAsync(GetEmailListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Email.EmailString);
            }

            var emails = await _emailRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _emailRepository.CountAsync()
                : await _emailRepository.CountAsync(email => email.EmailString.Contains(input.Filter));

            return new PagedResultDto<EmailDto>(
                totalCount,
                ObjectMapper.Map<List<Email>, List<EmailDto>>(emails)
            );
        }

        public async Task UpdateAsync(Guid id, CreateUpdateEmailDto input)
        {
            var email = await _emailRepository.GetAsync(id);

            if (email.EmailString != input.EmailString)
            {
                await _emailManager.ChangeNameAsync(email, input.EmailString);
            }

            email.Password = input.Password;

            await _emailRepository.UpdateAsync(email);
        }
    }
}
