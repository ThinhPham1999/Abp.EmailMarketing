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

            CreateMap<Pages.Contacts.CreateModalModel.CreateContactViewModel, CreateUpdateContactDto>();
            CreateMap<ContactDto, Pages.Contacts.EditModalModel.EditContactViewModel>();
            CreateMap<Pages.Contacts.EditModalModel.EditContactViewModel, CreateUpdateContactDto>();
        }
    }
}
