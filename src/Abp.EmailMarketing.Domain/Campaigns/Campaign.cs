using Abp.EmailMarketing.GroupContacts;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.EmailMarketing.Campaigns
{
    public class Campaign : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Content { get; set;}
        public DateTime Schedule { get; set; }

        public virtual IList<Group> Groups { get; set; }

        public Campaign()
        {

        }

        internal Campaign(
            Guid id,
            [NotNull] string name,
            string description,
            string title,
            string content,
            DateTime schedule)
            : base(id)
        {
            SetName(name);
            Description = description;
            Title = title;
            Content = content;
            Schedule = schedule;
        }

        internal Campaign(
            Guid id,
            [NotNull] string name,
            string description,
            string title,
            string content,
            DateTime schedule,
            List<Group> groups)
            : base(id)
        {
            SetName(name);
            Description = description;
            Title = title;
            Content = content;
            Schedule = schedule;
            Groups = groups;
        }

        internal Campaign ChangeName([NotNull] string name)
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
