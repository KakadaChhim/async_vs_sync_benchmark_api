using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace async_vs_sync_benchmark_api.Logics
{
    public class MaintenanceLogic(IServiceProvider provider) : SharedLogic(provider)
    {
        public async Task<string> UpgradeAsync()
        {
            await _db.Database.MigrateAsync();
            object migrationId = new();
            using (NpgsqlConnection conn = new(_db.Database.GetConnectionString()))
            {
                NpgsqlCommand cmd = new("SELECT \"MigrationId\" FROM \"Benchmark_Version\" ORDER BY \"MigrationId\" DESC LIMIT 1", conn);
                try
                {
                    conn.Open();
                    migrationId = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            return $"Migrate success {migrationId}";
        }
    }
}
