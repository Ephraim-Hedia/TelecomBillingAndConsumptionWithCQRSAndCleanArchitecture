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
            public const string RefreshToken = prefix + "Refresh-Token";
            public const string ValidateToken = prefix + "Validate-Token";
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
            public const string activate = prefix + "{id}/activate";
            public const string deactivate = prefix + "{id}/deactivate";
        }

        public static class Subscribers
        {
            public const string prefix = baseRoute + "subscribers/";
            public const string getAll = prefix;
            public const string updatePlan = prefix + "{id}/plan";
            public const string getAllPaginated = prefix + "Paginated";
            public const string getById = prefix + "{id}";
            public const string create = prefix;
            public const string update = prefix + "{id}";
            public const string delete = prefix + "{id}";
            public const string activate = prefix + "{id}/activate";
            public const string deactivate = prefix + "{id}/deactivate";
            public const string SubscriberUsageSummary = prefix + "{id}/SubscriberUsageSummary";
        }


        public static class UsageRecords
        {
            public const string prefix = baseRoute + "usageRecords/";
            public const string getAll = prefix;
            public const string getAllPaginated = prefix + "Paginated";
            public const string getById = prefix + "{id}";
            public const string getBySubscriberId = prefix + "subscriber/{subscriberId}";
            public const string create = prefix;
            public const string delete = prefix + "{id}";
        }

        public static class BillingRouting
        {
            public const string prefix = baseRoute + "billing/";
            public const string getAll = prefix;
            public const string getAllPaginated = prefix + "Paginated";
            public const string getById = prefix + "{id}";
            public const string getBySubscriberId = prefix + "subscriber/{subscriberId}";
            public const string getBySubscriberIdAndMonth = prefix + "subscriber/{subscriberId}/{month}";
            public const string getBillingHistoryForSubscriber = prefix + "subscriber/{subscriberId}/history";
            public const string getBillingDetailsByBillId = prefix + "{billId}/details";
            public const string getAllBillingsBySubscriberId = prefix + "{subscriberId}/all";
            public const string create = prefix;
            public const string update = prefix + "{id}";
            public const string delete = prefix + "{id}";
        }

        // GetDashboardOverviewQuery
        // GetDashboardRevenueQuery
        // GetDashboardTopCustomersPaginatedQuery
        // GetUsageStatisticsQuery

        public static class DashboardRouting
        {
            public const string prefix = baseRoute + "statistics/";
            public const string getOverview = prefix + "overview";
            public const string getRevenue = prefix + "revenue";
            public const string getTopCustomers = prefix + "top-customers";
            public const string getUsageStatistics = prefix + "usageStatistics";
        }

        // AddTariffRuleCommand
        // DeleteTariffRuleByIdCommand
        // UpdateTariffRuleByIdCommand
        // GetAllTariffsRulesQuery
        // GetTariffRuleByIdQuery

        public static class TariffRouting
        {
            public const string prefix = baseRoute + "tariffs/";
            public const string getAll = prefix;
            public const string getById = prefix + "{id}";
            public const string create = prefix;
            public const string update = prefix + "{id}";
            public const string delete = prefix + "{id}";
        }
    }


}
