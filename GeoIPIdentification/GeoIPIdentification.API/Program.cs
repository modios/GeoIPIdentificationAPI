using GeoIPIdentification.Applicaiton.Interfaces;
using GeoIPIdentification.Infrastructure.Persistence;
using GeoIPIdentification.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IIpBaseService, IpBaseService>();

builder.Services.AddDbContext<GeoIpDbContext>(options =>
    options.UseSqlite("Data Source=geoip.db"));

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Ensure logs are flushed
Log.CloseAndFlush();
