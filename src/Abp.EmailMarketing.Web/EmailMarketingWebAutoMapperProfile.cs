using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.GroupContacts;
using AutoMapper;

namespace Abp.EmailMarketing.Web
{
    public class EmailMarketingWebAutoMapperProfile : Profile
    {
        public EmailMarketingWebAutoMapperProfile()
        {
            CreateMap<ContactDto, CreateUpdateContactDto>();

            CreateMap<Pages.GroupContacts.CreateModalModel.CreateGroupViewModel, CreateGroupDto>();


            CreateMap<GroupDto, Pages.GroupContacts.EditModalModel.EditGroupViewModel>();
            CreateMap<Pages.GroupContacts.EditModalModel.EditGroupViewModel, UpdateGroupDto>();
        }
    }
}
