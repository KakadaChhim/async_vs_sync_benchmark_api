using Microsoft.AspNetCore.Mvc;
using async_vs_sync_benchmark_api.Models;
using async_vs_sync_benchmark_api.Logics;

namespace async_vs_sync_benchmark_api.Controllers
{
    public class UserController(IServiceProvider provider) : SharedController<UserLogic>(provider)
    {
        [HttpGet("async")]
        public async Task<List<UserListModel>> SearchAsync()
        {
            return await _logic.SearchAsync();
        }
        [HttpGet("sync")]
        public List<UserListModel> SearchSync()
        {
            return _logic.SearchSync();
        }
    }
}
