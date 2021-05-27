using System.Threading.Tasks;
using Abp.EmailMarketing.Localization;
using Abp.EmailMarketing.MultiTenancy;
using Abp.EmailMarketing.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Abp.EmailMarketing.Web.Menus
{
    public class EmailMarketingMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<EmailMarketingResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    EmailMarketingMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fas fa-home",
                    order: 0
                )
            );


            /*if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else*/
            {
#pragma warning disable CS0162 // Unreachable code detected
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
#pragma warning restore CS0162 // Unreachable code detected
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

            /*context.Menu.AddItem(
                new ApplicationMenuItem(
                    "EmailMarketing",
                    l["Menu:EmailMarketing"],
                    icon: "fa fa-book"
                ).AddItem(
                    new ApplicationMenuItem(
                        "EmailMarketing.Contacts",
                        l["Menu:Contacts"],
                        url: "/Contacts")
                  )
            );*/
            var emailMarketingMenu = new ApplicationMenuItem(
                "EmailMarketing",
                l["Menu:EmailMarketing"],
                icon: "fa fa-book"
            );

            context.Menu.AddItem(emailMarketingMenu);

            //Check the permission
            if (await context.IsGrantedAsync(EmailMarketingPermissions.Contacts.Default))
            {
                emailMarketingMenu.AddItem(new ApplicationMenuItem(
                    "EmailMarketing.Contacts",
                    l["Menu:Contacts"],
                    url: "/Contacts",
                    icon: "fas fa-users"
                ));
            }

            if (await context.IsGrantedAsync(EmailMarketingPermissions.Groups.Default))
            {
                emailMarketingMenu.AddItem(new ApplicationMenuItem(
                    "EmailMarketing.Groups",
                    l["Menu:Groups"],
                    url: "/GroupContacts",
                    icon: "fas fa-layer-group"
                ));
            }

            if (await context.IsGrantedAsync(EmailMarketingPermissions.Campaign.Default))
            {
                emailMarketingMenu.AddItem(new ApplicationMenuItem(
                    "EmailMarketing.Campaigns",
                    l["Menu:Campaigns"],
                    url: "/Campaigns",
                    icon: "fas fa-envelope"
                ));
            }

            if (await context.IsGrantedAsync(EmailMarketingPermissions.Email.Default))
            {
                emailMarketingMenu.AddItem(new ApplicationMenuItem(
                    "EmailMarketing.Emails",
                    l["Menu:Emails"],
                    url: "/Emails",
                    icon: "fas fa-user-friends"
                ));
            }

        }
    }
}
