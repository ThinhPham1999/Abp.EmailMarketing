using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Abp.EmailMarketing.GroupContacts
{
    public class GroupManager : DomainService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupManager(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> CreateAsync(
            [NotNull] string name,
            string description)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var existingGroup = await _groupRepository.FindByNameAsync(name);
            if (existingGroup != null)
            {
                throw new GroupAlreadyExistsException(name);
            }

            return new Group(
                GuidGenerator.Create(),
                name,
                description
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Group group,
            [NotNull] string newName)
        {
            Check.NotNull(group, nameof(group));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingGroup = await _groupRepository.FindByNameAsync(newName);
            if (existingGroup != null)
            {
                throw new GroupAlreadyExistsException(newName);
            }

            group.ChangeName(newName);
        }

    }
}
