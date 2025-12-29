using Microsoft.EntityFrameworkCore;
using async_vs_sync_benchmark_api.Helpers;
using static async_vs_sync_benchmark_api.Domains.BenchmarkDomain;

namespace async_vs_sync_benchmark_api
{
    public class BenchmarkDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BenchmarkDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings 
            options.UseNpgsql(_configuration.GetConnectionString(DatabaseConfigKeys.ConnectionString),
                x => x.MigrationsHistoryTable(DatabaseConfigKeys.TenantMgtVersion));
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.InitSomeData(_configuration);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName($"Benchmark_{entityType.GetTableName()}");
            }
        }

        public DbSet<User> Users { get; set; }
    }
    public static class DbContextSeedData
    {
        static readonly DateTime DefaultRowDate = new DateTime(2024, 9, 15);

        public static void InitSomeData(this ModelBuilder mb, IConfiguration configuration)
        {

        }
    }
}
