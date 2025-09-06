using BuildingBlocks.Common.Exceptions.Handlers;
using Cortex.Mediator.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore;

namespace Catalog.API;

public static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddCarter();

        builder.Services.AddCortexMediator(
            builder.Configuration,
            new[] { typeof(Startup) }, // Assemblies to scan
    options => options.AddDefaultBehaviors()
        );
        builder.Services.AddMarten(opts =>
        {
            opts.Connection(builder.Configuration.GetConnectionString("Database")!);
        }).UseLightweightSessions();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();

        builder.Services.AddExceptionHandler<AppExceptionHandler>();
        return builder.Build();
    }

    public static WebApplication AddPipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        app.MapOpenApi();

        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference();
            
        }
        
        app.UseHttpsRedirection();

        app.MapCarter();
        app.UseExceptionHandler(options => { });
        return app;
    }
}