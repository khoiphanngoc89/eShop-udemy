using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetConnectionString("Database");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionStrings);
        services.AddDbContext<OrderingDbContext>(c =>  c.UseSqlServer(connectionStrings));

        //services.AddDbContext<OrderContext>(c =>
        //    c.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")), ServiceLifetime.Singleton);
        //services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}
