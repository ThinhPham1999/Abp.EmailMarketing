﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Abp.EmailMarketing.Campaigns
{
    public class CampaignManager : DomainService
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignManager(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<Campaign> CreateAsync(
            [NotNull] string name,
            string description, DateTime dateTime, string title, string content)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var existingGroup = await _campaignRepository.FindByNameAsync(name);
            if (existingGroup != null)
            {
                throw new CampaignAlreadyExistsException(name);
            }

            return new Campaign(
                GuidGenerator.Create(),
                name,
                description,
                title,
                content,
                dateTime
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Campaign campaign,
            [NotNull] string newName)
        {
            Check.NotNull(campaign, nameof(campaign));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingGroup = await _campaignRepository.FindByNameAsync(newName);
            if (existingGroup != null)
            {
                throw new CampaignAlreadyExistsException(newName);
            }

            campaign.ChangeName(newName);
        }
    }
}
