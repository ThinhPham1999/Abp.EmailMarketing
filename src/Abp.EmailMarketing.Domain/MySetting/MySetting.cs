using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Abp.EmailMarketing.MySetting
{
    public class MySetting : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            var smtpHost = context.GetOrNull("Abp.Mailing.Smtp.Host");
            if (smtpHost != null)
            {
                smtpHost.DefaultValue = "mail.mydomain.com";
                /*smtpHost.DisplayName =
                    new LocalizableString(
                        typeof(EmailMarketingLocalizationResource),
                        "SmtpServer_DisplayName"
                    );*/
            }
        }
    }
}
