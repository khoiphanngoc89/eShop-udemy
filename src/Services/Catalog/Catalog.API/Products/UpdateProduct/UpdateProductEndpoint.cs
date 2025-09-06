
namespace Catalog.API.Products.UpdateProduct;
public sealed record UpdateProductRequest(Guid Id, string Name, IEnumerable<string> Categories, string Description, string ImageFile, decimal Price);
public sealed record UpdateProductResponse(bool IsSuccess);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var result = await sender.Send(request.Adapt<UpdateProductCommand>());
            var resp = new UpdateProductResponse(result.IsSuccess);
            return Results.Ok(resp);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update an existing product")
        .WithDescription("Update an existing product");
    }
}
