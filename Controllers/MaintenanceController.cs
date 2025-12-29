using Microsoft.AspNetCore.Mvc;
using async_vs_sync_benchmark_api.Logics;

namespace async_vs_sync_benchmark_api.Controllers
{
    public class MaintenanceController(IServiceProvider provider) : SharedController<MaintenanceLogic>(provider)
    {
        [HttpPost("migrate")]
        public async Task<string> ApplyMigrations()
        {
            return await _logic.UpgradeAsync();
        }
    }
}
