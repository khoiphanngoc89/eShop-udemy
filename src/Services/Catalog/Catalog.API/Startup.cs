using BuildingBlocks.Common.Exceptions.Handlers;
using BuildingBlocks.Common.Exceptions.Middlewares;
using Marten;
using Weasel.Core;

namespace Catalog.API;

public static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddCarter();
        builder.Services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        builder.Services.AddMarten(opts =>
        {
            opts.Connection(builder.Configuration.GetConnectionString("Database")!);
        }).UseLightweightSessions();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddExceptionHandler<AppExceptionHandler>();
        return builder.Build();
    }

    public static WebApplication AddPipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.MapCarter();
        app.UseExceptionHandler(options => { });
        return app;
    }
}