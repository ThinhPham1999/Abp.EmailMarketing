using Volo.Abp.Settings;

namespace Abp.EmailMarketing.Settings
{
    public class EmailMarketingSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(EmailMarketingSettings.MySetting1));
        }
    }
}
