namespace async_vs_sync_benchmark_api.Helpers
{
    /// <summary>
    /// Keys related to Database configuration.
    /// </summary>
    public static class DatabaseConfigKeys
    {
        public const string ConnectionString = "DefaultConnection"; 
        public const string TenantMgtVersion = "Benchmark_Version";
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SecurityConfigKeys
    {
        public const string _Root = "Security:";
        public const string ReqTime = "Security:ReqTime";
        public const string ReqTimeEnable = "Security:ReqTimeEnable";
    }
}
