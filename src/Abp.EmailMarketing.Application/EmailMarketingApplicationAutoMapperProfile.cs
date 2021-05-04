using Abp.EmailMarketing.Contacts;
using AutoMapper;

namespace Abp.EmailMarketing
{
    public class EmailMarketingApplicationAutoMapperProfile : Profile
    {
        public EmailMarketingApplicationAutoMapperProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<CreateUpdateContactDto, Contact>();
        }
    }
}
