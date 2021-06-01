using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.EmailMarketing.Emails
{
    public class Email : FullAuditedAggregateRoot<Guid>
    {
        public string EmailString { get; private set; }
        public string Password { get; set; }
        public int Order { get; set; }

        /*private Email()
        {

        }*/

        public Email()
        {

        }

        internal Email(
            Guid id,
            [NotNull] string emailString,
            [NotNull] string password,
            int order
            )
        {
            SetEmailString(emailString);
            Password = password;
            Order = order;
        }

        internal Email ChangeEmailString([NotNull] string emailString)
        {
            SetEmailString(emailString);
            return this;
        }

        private void SetEmailString([NotNull] string emailString)
        {
            EmailString = Check.NotNullOrWhiteSpace(
                emailString,
                nameof(emailString),
                maxLength: EmailConsts.MaxEmailStringLength
            );
        }
    }
}
