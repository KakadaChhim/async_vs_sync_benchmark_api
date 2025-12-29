using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Serilog;
using async_vs_sync_benchmark_api.Configs;
using async_vs_sync_benchmark_api.Extensions;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();

// Serilog config.
var logPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "logs/log.txt");
Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollOnFileSizeLimit: true, fileSizeLimitBytes: 20 * 1024 * 1024)
                .CreateLogger();
builder.Services.AddLogging(cnf =>
{
    cnf.AddSerilog(Log.Logger);
});

// Add AutoMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperConfig>();
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add logic service
builder.AddLogics();

// Add services to the container.
builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
    {
        // 2017-01-25T07:00:00Z             UTC 
        // 2017-01-25T07:00:00              Unspecified
        // 2017-01-25T07:00:00+07:00:00     Local GMT+7 PhnomPenh
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
    });
builder.Services.AddHttpContextAccessor();

// Add services to the container.
// Lowercase url.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Allow Cors.
// Redis Exchange Cache
var corsOrigins = builder.Configuration.GetSection("CorsOrigins").Value;
var origins = corsOrigins.Split(";");
Log.Information("Register CORS origins {origins}", origins);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "corPolicy",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});

builder.Services.AddControllers();

// Learn more at https://aka.ms/aspnetcore/swashbuckle
builder.AddSwaggerInfo();

var app = builder.Build();
//Add corss origin
app.UseCors("corPolicy");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();
var userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var filePath = Path.Combine(userDirectory, builder.Configuration.GetSection("FileUpload:FilePath").Value);
if (!Directory.Exists(filePath))
{
    Directory.CreateDirectory(filePath!);
}
var wwwPath = Path.Combine(filePath);
if (!Directory.Exists(wwwPath))
{
    Directory.CreateDirectory(wwwPath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(wwwPath),
    RequestPath = "/files"
});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

Log.CloseAndFlush();

