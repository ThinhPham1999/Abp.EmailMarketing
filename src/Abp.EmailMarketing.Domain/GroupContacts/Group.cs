using Abp.EmailMarketing.Campaigns;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.EmailMarketing.GroupContacts
{
    public class Group : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; set; }

        public virtual IList<Campaign> Campaigns { get; set; }

        public Group()
        {

        }

        internal Group(
            Guid id,
            [NotNull] string name,
            string description)
            :base(id)
        {
            SetName(name);
            Description = description;
        }

        internal Group ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: GroupConsts.MaxNameLength
            );
        }
    }
}
