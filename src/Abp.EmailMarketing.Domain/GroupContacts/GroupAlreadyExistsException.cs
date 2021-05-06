using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp.EmailMarketing.GroupContacts
{
    class GroupAlreadyExistsException : BusinessException
    {
        public GroupAlreadyExistsException(string name):
            base(EmailMarketingDomainErrorCodes.GroupAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
