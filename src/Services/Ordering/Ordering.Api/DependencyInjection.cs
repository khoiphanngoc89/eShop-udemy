using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering.Api;

public static class DependencyInjection
{
    internal static WebApplication ConfigureApiServices(this WebApplicationBuilder builder)
    {
        //services.AddApplicationServices(configuration);
        //services.AddInfrastructureServices(configuration);
        //services.AddControllers();
        //services.AddSwaggerGen(c =>
        //{
        //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.Api", Version = "v1" });
        //});
        builder.Services.AddServices()
            .AddInfrastructureServices(builder.Configuration)
            .AddApplicationServices();
        return builder.Build();
    }

    internal static IServiceCollection AddServices(this IServiceCollection services)
    {

        return services;
    }

    internal static WebApplication ConfigurePipeline(this WebApplication app)
    {
        //// use swagger
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        //app.MapCarter();

        //// use http logging
        //app.UseHttpLogging();

        //// using cutting-cross concerns
        //app.UseExceptionHandler(opts => { });
        //app.UseHealthChecks("/health", new HealthCheckOptions
        //{
        //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //});

        return app;
    }
}
