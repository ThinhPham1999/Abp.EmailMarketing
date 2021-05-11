﻿using Abp.EmailMarketing.Campaigns;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.GroupContacts;
using AutoMapper;

namespace Abp.EmailMarketing
{
    public class EmailMarketingApplicationAutoMapperProfile : Profile
    {
        public EmailMarketingApplicationAutoMapperProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<CreateUpdateContactDto, Contact>();
            CreateMap<Group, GroupDto>();
            CreateMap<Group, GroupLookupDto>();

            CreateMap<Campaign, CampaignDto>();
        }
    }
}
