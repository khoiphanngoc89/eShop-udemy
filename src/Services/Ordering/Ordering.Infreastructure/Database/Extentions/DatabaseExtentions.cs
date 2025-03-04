using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Database.Extentions;

public static class DatabaseExtentions
{
    public static async Task<WebApplication> UseMigrateAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();
        await context.Database.MigrateAsync();
        await SendAsync(context);
        return app;
    }

    private static async Task SendAsync(OrderingDbContext context)
    {
        await SeedCustomerAsync(context);
        await SeedProductAsync(context);
        await SeedOrderWithItemsAsync(context);
    }

    private static async Task SeedOrderWithItemsAsync(OrderingDbContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(InitialData.Orders);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductAsync(OrderingDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCustomerAsync(OrderingDbContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(InitialData.Customers);
            await context.SaveChangesAsync();
        }
    }
}
