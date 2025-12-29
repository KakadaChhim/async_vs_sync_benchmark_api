using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using async_vs_sync_benchmark_api.Helpers;
using async_vs_sync_benchmark_api.Logics;

namespace async_vs_sync_benchmark_api.Extensions
{
    public static class BuilderExtension
    {
        public static void AddSwaggerInfo(this WebApplicationBuilder builder)
        {
            // access assembly without type reference.
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fileInfo = new FileInfo(assembly.Location);
            var info = new OpenApiInfo()
            {
                Title = "tenant-mgt-api",
                Description = $"Latest update: {fileInfo.LastWriteTime:o}",
            };
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", info);
                var prefix = builder.Configuration.GetSection("Swagger")
                          .GetSection("RoutePrefix").Value;
                if (!string.IsNullOrEmpty(prefix))
                {
                    o.DocumentFilter<RoutePrefixDocumentFilter>(prefix.ToLower());
                }
            });
        }
        public static void AddLogics(this WebApplicationBuilder builder)
        {
            var logics = AppDomain.CurrentDomain.GetAssemblies()
                                                .SelectMany(t => t.GetTypes())
                                                .Where(t => t.IsClass
                                                        && !t.IsAbstract // Ensure we don't try to register abstract classes
                                                        && typeof(SharedLogic).IsAssignableFrom(t) // Checks if it inherits from SharedLogic
                                                        && t != typeof(SharedLogic));
            foreach (var logic in logics)
            {
                builder.Services.AddScoped(logic);
            }
            var connectionString = builder.Configuration.GetConnectionString(DatabaseConfigKeys.ConnectionString);
            builder.Services.AddDbContext<BenchmarkDbContext>(x => x.UseNpgsql(connectionString)); ;
        }
        public class RoutePrefixDocumentFilter : IDocumentFilter
        {
            private readonly string _routePrefix;

            public RoutePrefixDocumentFilter(string routePrefix)
            {
                _routePrefix = routePrefix;
            }

            // Append the prefix to the existing paths
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var paths = new OpenApiPaths();
                foreach (var path in swaggerDoc.Paths)
                {
                    paths.Add($"/{_routePrefix}{path.Key}", path.Value);
                }
                swaggerDoc.Paths = paths;
            }
        }
    }
}
