using Abp.EmailMarketing.Campaigns;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.Emails;
using Abp.EmailMarketing.GroupContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Abp.EmailMarketing
{
    public class EmailMarketingDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Contact, Guid> _contactRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly GroupManager _groupManager;
        private readonly ICampaignRepository _campaignRepository;
        private readonly CampaignManager _campaignManager;
        private readonly IEmailRepository _emailRepository;
        private readonly EmailManager _emailManager;

        public EmailMarketingDataSeederContributor(IRepository<Contact, Guid> contactRepository
            , IGroupRepository groupRepository, GroupManager groupManager,
            ICampaignRepository campaignRepository, CampaignManager campaignManager,
            IEmailRepository emailRepository, EmailManager emailManager)
        {
            _contactRepository = contactRepository;
            _groupManager = groupManager;
            _groupRepository = groupRepository;
            _campaignRepository = campaignRepository;
            _campaignManager = campaignManager;
            _emailRepository = emailRepository;
            _emailManager = emailManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _contactRepository.GetCountAsync() > 0)
            {
                return;
            }

            //Add group to db
            if (await _groupRepository.GetCountAsync() <= 0)
            {
                var group03 = await _groupRepository.InsertAsync(
                    await _groupManager.CreateAsync(
                        "Group03",
                        "This is group 3"
                    )
                );

                var group04 = await _groupRepository.InsertAsync(
                     await _groupManager.CreateAsync(
                         "Group04",
                         "This is group 4"
                     )
                 );
                //Add contact to db
                if (await _contactRepository.GetCountAsync() <= 0)
                {
                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            GroupId = group03.Id,
                            Email = "thinhphu1234567@gmail.com",
                            FirstName = "Pham",
                            LastName = "Thinh",
                            DateOfBirth = new DateTime(1999, 5, 21),
                            PhoneNumber = "0905901869",
                            Type = ContactType.Group01
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            GroupId = group04.Id,
                            Email = "beni09082004@gmail.com",
                            FirstName = "Bich",
                            LastName = "Tram",
                            DateOfBirth = new DateTime(2004, 8, 9),
                            PhoneNumber = "0932443774",
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );
                };
            }
            

            if (await _campaignRepository.GetCountAsync() <= 0)
            {
                await _campaignRepository.InsertAsync(
                    await _campaignManager.CreateAsync(
                        "Campaign01",
                        "This is Campaign 1",
                        new DateTime(2021, 5, 11),
                        "abcxyz",
                        "Title01"
                    )
                    ,
                    autoSave: true
                );

                await _campaignRepository.InsertAsync(
                    await _campaignManager.CreateAsync(
                        "Campaign02",
                        "This is Campaign 2",
                        new DateTime(2021, 05, 10),
                        "abcxyz123",
                        "Title02"
                    )
                    ,
                    autoSave: true
                );
            }

            if (await _emailRepository.GetCountAsync() <= 0)
            {
                var email = await _emailManager.CreateAsync(
                        "vothithuqua11121997@gmail.com",
                        "thuqua1997",
                        0
                    );
                await _emailRepository.InsertAsync(
                    email,
                    autoSave: true
                );
                await _emailRepository.InsertAsync(
                    await _emailManager.CreateAsync(
                        "tranducbo17a1.11@gmail.com",
                        "Abc123#!",
                        0
                    ),
                    autoSave: true
                );
                await _emailRepository.InsertAsync(
                    await _emailManager.CreateAsync(
                        "ntthao@sdc.udn.vn",
                        "Sdc@2021",
                        0
                    ),
                    autoSave: true
                );
            }

        }
    }
}
