using Abp.EmailMarketing.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.EmailMarketing.Permissions
{
    public class EmailMarketingPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var EmailMarketingGroup = context.AddGroup(EmailMarketingPermissions.GroupName, L("Permission:EmailMarketing"));

            //Define your own permissions here. Example:
            var contactsPermisson = EmailMarketingGroup.AddPermission(EmailMarketingPermissions.Contacts.Default, L("Permission:Contacts"));
            contactsPermisson.AddChild(EmailMarketingPermissions.Contacts.Create, L("Permission:Contacts.Create"));
            contactsPermisson.AddChild(EmailMarketingPermissions.Contacts.Edit, L("Permission:Contacts.Edit"));
            contactsPermisson.AddChild(EmailMarketingPermissions.Contacts.Delete, L("Permission:Contacts.Delete"));


            var groupsPermission = EmailMarketingGroup.AddPermission(EmailMarketingPermissions.Groups.Default, L("Permission:Groups"));
            groupsPermission.AddChild(EmailMarketingPermissions.Groups.Create, L("Permission:Groups.Create"));
            groupsPermission.AddChild(EmailMarketingPermissions.Groups.Edit, L("Permission:Groups.Edit"));
            groupsPermission.AddChild(EmailMarketingPermissions.Groups.Delete, L("Permission:Groups.Delete"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EmailMarketingResource>(name);
        }
    }
}
