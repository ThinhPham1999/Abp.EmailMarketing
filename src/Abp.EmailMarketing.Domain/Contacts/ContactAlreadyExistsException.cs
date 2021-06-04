using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp.EmailMarketing.Contacts
{
    public class ContactAlreadyExistsException : BusinessException
    {
        public ContactAlreadyExistsException(string name) :
            base(EmailMarketingDomainErrorCodes.ContactAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
