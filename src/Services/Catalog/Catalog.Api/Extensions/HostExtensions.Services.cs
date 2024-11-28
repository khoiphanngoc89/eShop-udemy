﻿using BuildingBlocks.Cores.Exceptions.Handlers;
using Catalog.Api.Persistence;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;

namespace Catalog.Api.Extensions;

public static partial class HostExtensions
{
    internal static WebApplication AddService(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddCarter(new DependencyContextAssemblyCatalogCustom());

        var assembly = typeof(Program).Assembly;
        builder.Services.AddMediatR(config =>
        {
            // The MediatR will auto scan running assembly to register the Request and Command services are located
            config.RegisterServicesFromAssembly(assembly);
            // add the PipleBehavior into MediatR
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        // Add Fluent Validation in the service
        builder.Services.AddValidatorsFromAssembly(assembly);

        var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionStrings);

        // register database and its migration
        builder.Services.AddDatabase(connectionStrings);

        // Register global exception handler
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // Add health check for this service
        builder.Services.AddHealthChecks()
            .AddNpgSql(connectionStrings);

        // create a IHost base on the services and essential configurations
        // not yet listen on http/https
        return builder.Build();
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, string connectionStrings)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IWebHostEnvironment? env = serviceProvider.GetService<IWebHostEnvironment>();

        services.AddMarten(opts =>
        {
            opts.Connection(connectionStrings);
        }).UseLightweightSessions();

        if (env?.IsDevelopment() == true)
        {
            services.InitializeMartenWith<CatalogInitialData>();
        }
        return services;
    }
}
