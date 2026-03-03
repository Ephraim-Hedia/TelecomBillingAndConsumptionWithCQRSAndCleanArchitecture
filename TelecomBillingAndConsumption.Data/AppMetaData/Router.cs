namespace TelecomBillingAndConsumption.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "api";
        public const string version = "v1";
        public const string baseRoute = root + "/" + version + "/";

        public static class ApplicationUserRouting
        {
            public const string prefix = baseRoute + "user/";
            public const string getAll = prefix;
            public const string getAllPaginated = prefix + "Paginated";
            public const string getById = prefix + "{id}";
            public const string create = prefix;
            public const string update = prefix;
            public const string delete = prefix + "{id}";
            public const string changePassword = prefix + "changePassword";
        }
        public static class AuthenticationRouting
        {
            public const string prefix = baseRoute + "auth/";
            public const string signIn = prefix + "signIn";
            public const string signOut = prefix + "signOut";
        }


        public static class PlansRouting
        {
            public const string prefix = baseRoute + "plans/";
            public const string getAll = prefix;
            public const string getAllPaginated = prefix + "Paginated";
            public const string getById = prefix + "{id}";
            public const string create = prefix;
            public const string update = prefix + "{id}";
            public const string delete = prefix + "{id}";
        }
    }
}
