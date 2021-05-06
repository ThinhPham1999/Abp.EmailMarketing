using Abp.EmailMarketing.Contacts;
using AutoMapper;

namespace Abp.EmailMarketing.Web
{
    public class EmailMarketingWebAutoMapperProfile : Profile
    {
        public EmailMarketingWebAutoMapperProfile()
        {
            CreateMap<ContactDto, CreateUpdateContactDto>();
        }
    }
}
