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

                var group01 = await _groupRepository.InsertAsync(
                     await _groupManager.CreateAsync(
                         "Group01",
                         "This is group 1"
                     )
                 );
                var group02 = await _groupRepository.InsertAsync(
                     await _groupManager.CreateAsync(
                         "Group02",
                         "This is group 2"
                     )
                 );
                var group05 = await _groupRepository.InsertAsync(
                     await _groupManager.CreateAsync(
                         "Group05",
                         "This is group 5"
                     )
                 );

                //Add contact to db
                if (await _contactRepository.GetCountAsync() <= 0)
                {
                    /*List<ContactGroup> groups1 = new List<ContactGroup>();
                    groups1.Add(new ContactGroup() 
                    { 
                        
                    });
                    groups1.Add(group02);
                    List<Group> groups2 = new List<Group>();
                    groups2.Add(group03);
                    groups2.Add(group02);
                    List<Group> groups3 = new List<Group>();
                    groups3.Add(group04);
                    List<Group> groups4 = new List<Group>();
                    groups3.Add(group05);
                    groups3.Add(group01);*/

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Contact//Groups = groups1,
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
                            ////Groups = groups2,
                            Email = "beni09082004@gmail.com",
                            FirstName = "Bich",
                            LastName = "Tram",
                            DateOfBirth = new DateTime(2004, 8, 9),
                            PhoneNumber = "0932443774",
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            ////Groups = groups3,
                            Email = "thinhpvpde130111@fpt.edu.vn",
                            FirstName = "Pham",
                            LastName = "Thinh",
                            DateOfBirth = new DateTime(1999, 5, 21),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            ////Groups = groups4,
                            Email = "swuull99@gmail.com",
                            FirstName = "Hoàng",
                            LastName = "Hiệp",
                            DateOfBirth = new DateTime(1999, 4, 30),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            ////Groups = groups3,
                            Email = "vantcde130099@fpt.edu.vn",
                            FirstName = "Công",
                            LastName = "Văn",
                            DateOfBirth = new DateTime(1999, 4, 27),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            ////Groups = groups1,
                            Email = "vantran99d@gmail.com",
                            FirstName = "Tran Công",
                            LastName = "Văn",
                            DateOfBirth = new DateTime(1999, 4, 27),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            ////Groups = groups4,
                            Email = "hungpqDE130125@fpt.edu.vn",
                            FirstName = "Pham",
                            LastName = "Hưng",
                            DateOfBirth = new DateTime(1998, 1, 27),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                           // //Groups = groups2,
                            Email = "minhvhDE130134@fpt.edu.vn",
                            FirstName = "Vũ",
                            LastName = "Minh",
                            DateOfBirth = new DateTime(1999, 2, 27),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups1,
                            Email = "haonmde130144@fpt.edu.vn",
                            FirstName = "Minh",
                            LastName = "Hào",
                            DateOfBirth = new DateTime(1999, 3, 20),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups3,
                            Email = "lamntDE130128@fpt.edu.vn",
                            FirstName = "Trí",
                            LastName = "Lâm",
                            DateOfBirth = new DateTime(1999, 5, 30),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups3,
                            Email = "anhpnDE130131@fpt.edu.vn",
                            FirstName = "Nhật",
                            LastName = "Anh",
                            DateOfBirth = new DateTime(1999, 5, 30),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups4,
                            Email = "minhnhde130140@fpt.edu.vn",
                            FirstName = "Nguyễn",
                            LastName = "Minh",
                            DateOfBirth = new DateTime(1999, 5, 30),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups1,
                            Email = "namptDE130115@fpt.edu.vn",
                            FirstName = "Trung",
                            LastName = "Nam",
                            DateOfBirth = new DateTime(1999, 5, 30),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups2,
                            Email = "longtvDE130118@fpt.edu.vn",
                            FirstName = "Việt",
                            LastName = "Long",
                            DateOfBirth = new DateTime(1999, 1, 30),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups4,
                            Email = "hiepmhDE130105@fpt.edu.vn",
                            FirstName = "Hoàng",
                            LastName = "Hiệp",
                            DateOfBirth = new DateTime(1999, 1, 30),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups3,
                            Email = "huypcDE130102@fpt.edu.vn",
                            FirstName = "Công",
                            LastName = "Huy",
                            DateOfBirth = new DateTime(1999, 1, 25),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups3,
                            Email = "cuongnmde130123@fpt.edu.vn",
                            FirstName = "Manh",
                            LastName = "Cường",
                            DateOfBirth = new DateTime(1999, 1, 20),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups1,
                            Email = "huyndde140196@fpt.edu.vn",
                            FirstName = "Đức",
                            LastName = "Huy",
                            DateOfBirth = new DateTime(1999, 7, 20),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups3,
                            Email = "khoatpaDE130107@fpt.edu.vn",
                            FirstName = "Anh",
                            LastName = "Khoa",
                            DateOfBirth = new DateTime(1999, 7, 20),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups3,
                            Email = "duynhtDE130023@fpt.edu.vn",
                            FirstName = "Thế",
                            LastName = "Duy",
                            DateOfBirth = new DateTime(1999, 7, 20),
                            Type = ContactType.Group02
                        },
                        autoSave: true
                    );

                    await _contactRepository.InsertAsync(
                        new Contact
                        {
                            //Groups = groups2,
                            Email = "trambich0935@gmail.com",
                            FirstName = "Bích",
                            LastName = "Trâm",
                            DateOfBirth = new DateTime(2004, 7, 8),
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
