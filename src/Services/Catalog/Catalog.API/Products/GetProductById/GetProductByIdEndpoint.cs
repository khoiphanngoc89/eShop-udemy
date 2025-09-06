using Cortex.Mediator;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdRespose(Product Product);

public sealed class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (IMediator sender, Guid id) =>
        {
            var result = await sender.SendQueryAsync<GetProductByIdQuery, GetProductByIdResult>(new GetProductByIdQuery(id));
            return Results.Ok(result.Adapt<GetProductByIdRespose>());
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdRespose>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get product by id")
        .WithDescription("Get product by id");
    }
}