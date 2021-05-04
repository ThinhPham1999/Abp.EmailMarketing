using Abp.EmailMarketing.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.EmailMarketing.Permissions
{
    public class EmailMarketingPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(EmailMarketingPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(EmailMarketingPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EmailMarketingResource>(name);
        }
    }
}
