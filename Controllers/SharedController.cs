using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace async_vs_sync_benchmark_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SharedController<TLogic> : ControllerBase where TLogic : class
    {
        public readonly BenchmarkDbContext _db;
        public readonly IMapper _mapper;
        public readonly TLogic _logic;
        public readonly IServiceProvider _provider;
        public SharedController(IServiceProvider provider)
        {
            _db = provider.GetRequiredService<BenchmarkDbContext>();
            _mapper = provider.GetRequiredService<IMapper>();
            _logic = provider.GetRequiredService<TLogic>();
            _provider = provider;
        }
    }
}

