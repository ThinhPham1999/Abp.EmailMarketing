namespace Abp.EmailMarketing.Permissions
{
    public static class EmailMarketingPermissions
    {
        public const string GroupName = "EmailMarketing";

        public static class Contacts
        {
            public const string Default = GroupName + ".Contacts";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        // *** ADDED a NEW NESTED CLASS ***
        public static class Groups
        {
            public const string Default = GroupName + ".Groups";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Campaign
        {
            public const string Default = GroupName + ".Campaigns";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }


    }
}