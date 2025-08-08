using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
            //services.AddAutoMapper(typeof(MappingProfile));
        //services.AddTransient<IOrderService, OrderService>();
        //services.AddTransient<ICheckoutService, CheckoutService>();
        //services.AddTransient<IOrderRepository, OrderRepository>();
        return services;
    }
}
