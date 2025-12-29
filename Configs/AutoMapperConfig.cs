using async_vs_sync_benchmark_api.Models;
using AutoMapper;
using static async_vs_sync_benchmark_api.Domains.BenchmarkDomain;

namespace async_vs_sync_benchmark_api.Configs
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserAddModel>().ReverseMap();
            CreateMap<User, UserEditModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserListModel>().ReverseMap();
        }
    }
}

