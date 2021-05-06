﻿using Abp.EmailMarketing.Contacts;
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

        public EmailMarketingDataSeederContributor(IRepository<Contact, Guid> contactRepository
            , IGroupRepository groupRepository, GroupManager groupManager)
        {
            _contactRepository = contactRepository;
            _groupManager = groupManager;
            _groupRepository = groupRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            //Add contact to db
            if (await _contactRepository.GetCountAsync() <= 0)
            {
                await _contactRepository.InsertAsync(
                    new Contact
                    {
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
                        Email = "beni09082004@gmail.com",
                        FirstName = "Bich",
                        LastName = "Tram",
                        DateOfBirth = new DateTime(2004, 8, 9),
                        PhoneNumber = "0932443774",
                        Type = ContactType.Group02
                    },
                    autoSave: true
                );
            }

            //Add group to db
            if (await _groupRepository.CountAsync() <= 0)
            {
                await _groupRepository.InsertAsync(
                    await _groupManager.CreateAsync(
                        "Group01",
                        "This is group 1"
                    )
                );

                await _groupRepository.InsertAsync(
                   await _groupManager.CreateAsync(
                       "Group02",
                       "This is group 2"
                   )
               );
            }

        }
    }
}
