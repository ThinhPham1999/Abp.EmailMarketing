using Abp.EmailMarketing.Campaigns;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.Emails;
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

            CreateMap<Pages.Campaigns.CreateCampaignModel.CreateCampaignViewModel, CreateUpdateCampaignDto>();
            CreateMap<CampaignDto, Pages.Campaigns.ViewDetailModel.ViewDetailCampaignViewModel>();

            CreateMap<EmailDto, Pages.Emails.EditModalModel.EditEmailViewModel>();
            CreateMap<Pages.Emails.EditModalModel.EditEmailViewModel, CreateUpdateEmailDto>();
            CreateMap<Pages.Emails.CreateModalModel.CreateEmailViewModel, CreateUpdateEmailDto>();

            CreateMap<ContactDto, Pages.GroupContacts.ContactViewModel.ContactView>();

            CreateMap<Pages.GroupContacts.ContactViewModel.SaveModel, UpdateContactInGroupDto>();
        }
    }
}
