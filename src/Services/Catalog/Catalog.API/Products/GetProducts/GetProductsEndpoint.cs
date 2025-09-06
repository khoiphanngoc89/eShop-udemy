using Cortex.Mediator;

namespace Catalog.API.Products.GetProduct;

public sealed record GetProductsRespose(IEnumerable<Product> Products);

public sealed class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (IMediator sender) =>
        {
            var result = await sender.SendQueryAsync<GetProductsQuery, GetProductsResult>(new GetProductsQuery());
            return Results.Ok(result.Adapt<GetProductsRespose>());
        }).WithName("GetProducts")
        .Produces<GetProductsRespose>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
