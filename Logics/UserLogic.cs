using Microsoft.EntityFrameworkCore;
using async_vs_sync_benchmark_api.Models;

namespace async_vs_sync_benchmark_api.Logics
{
    public class UserLogic(IServiceProvider provider) : SharedLogic(provider)
    {
        public async Task<List<UserListModel>> SearchAsync() 
        {
            var q = _db.Users.Where(x => x.Active);
            return _mapper.Map<List<UserListModel>>(await q.ToListAsync());
        }
        public List<UserListModel> SearchSync()
        {
            var q = _db.Users.Where(x => x.Active);
            return _mapper.Map<List<UserListModel>>(q.ToList());
        }
    }
}
