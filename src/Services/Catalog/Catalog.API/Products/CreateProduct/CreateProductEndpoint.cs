using Cortex.Mediator;

namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductRequest(string Name, IEnumerable<string> Categories, string Description, string ImageFile, decimal Price);
public sealed record CreateProductResponse(Guid Id);

public sealed class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, IMediator sender) =>
        {
            var result= await sender.SendCommandAsync<CreateProductCommand, CreateProductResult>(request.Adapt<CreateProductCommand>());
            var resp = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{resp.Id}", resp);
        })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new product")
            .WithDisplayName("Create a new product");
        


    }
}