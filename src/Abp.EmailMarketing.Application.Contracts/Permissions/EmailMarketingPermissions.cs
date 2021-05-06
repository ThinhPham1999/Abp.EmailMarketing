namespace Abp.EmailMarketing.Permissions
{
    public static class EmailMarketingPermissions
    {
        public const string GroupName = "EmailMarketing";

        public static class Contacts
        {
            public const string Default = GroupName + ".Contact";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

    }
}