using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using async_vs_sync_benchmark_api.Domain;
using static async_vs_sync_benchmark_api.Models.SharedModel;

namespace async_vs_sync_benchmark_api.Logics
{
	public class SharedLogic
	{
        public const int CACHE_AGE_QUICK = 3 * 60; // 3 minutes
        public const int CACHE_AGE_MEDIUM = 1 * 60 * 60; // 60 minutes = 1 hours
        public const int CACHE_AGE_MAX = 30 * 24 * 60 * 60; // 30 days = 30 * 24 hours

        protected readonly BenchmarkDbContext _db;
        protected readonly IMapper _mapper;
        protected readonly IServiceProvider _provider;
        protected readonly HttpContext _httpContext;
        protected readonly string FILE_PATH;
        protected readonly IConfiguration _config;

        public SharedLogic(IServiceProvider provider)
        {
            _provider = provider;
            _db = provider.GetService<BenchmarkDbContext>();
            _mapper = provider.GetService<IMapper>();
            _httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
            _config = provider.GetService<IConfiguration>();
            FILE_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "www/files");
            if (!Directory.Exists(FILE_PATH))
            {
                Directory.CreateDirectory(FILE_PATH);
            }
        }
        public void UpdateField<TDomain, TModel>(TDomain entry, TModel model) where TDomain : SharedDomain, new()
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                // has column in model but not have in entity
                if (entry.GetType().GetProperty(prop.Name) == null)
                {
                    continue;
                }
                // same column different type.
                if (prop.PropertyType != entry.GetType().GetProperty(prop.Name)?.PropertyType)
                {
                    continue;
                }

                entry.GetType().GetProperty(prop.Name)?.SetValue(entry, prop.GetValue(model, null));
            }
            entry.LastModifiedDate = DateTime.Now;
        }
        public void RemoveEntry<TDomain>(TDomain entry) where TDomain : SharedDomain, new()
        {
            entry.Active = false;
            entry.LastModifiedDate = DateTime.Now;
        }
        protected ApiResponse<T> ErrorResponse<T>(string message, ResponseType responseType, int code = 500) =>
            new() { Result = default, Message = message, ResponseType = ResponseType.Error , StatusCode = code };
        protected ViewResult<T> Error<T>(string message, ResponseType responseType, int code = 500) =>
            new() { Result = default, Message = message, ResponseType = ResponseType.Error, StatusCode = code };
        protected string getCustomCachePath(string customKey)
        {
            var appCode = _db.Database.GetDbConnection().Database;
            var key = string.Concat(appCode,"-",customKey).ToLower();
            return key;
        }
    }
}

