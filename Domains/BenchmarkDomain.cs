using async_vs_sync_benchmark_api.Domain;

namespace async_vs_sync_benchmark_api.Domains
{
    public class BenchmarkDomain
    {
        public class User: SharedDomain
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
