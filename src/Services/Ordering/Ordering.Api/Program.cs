using Common.Logging;
using Ordering.Api;
using Ordering.Infrastructure.Database.Extentions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container

builder.Host.UseSerilog(Serilogger.Configure);

var app = builder.ConfigureApiServices().ConfigurePipeline();

if (app.Environment.IsDevelopment())
{
    await app.UseMigrateAsync();
}

// Configure the HTTP request pipeline

app.Run();
