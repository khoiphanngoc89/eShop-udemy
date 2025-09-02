namespace Catalog.API.Products.GetProductById;

public record GetProductByIdRespose(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price);

public sealed class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            return Results.Ok(result.Adapt<GetProductByIdRespose>());
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdRespose>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get product by id")
        .WithDescription("Get product by id");
    }
}
