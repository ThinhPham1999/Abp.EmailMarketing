using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.EmailMarketing.Emails
{
    public class Email
    {
        public int ID { get; set; }
        public string EmailString { get; private set; }

        private Email()
        {

        }
    }
}
