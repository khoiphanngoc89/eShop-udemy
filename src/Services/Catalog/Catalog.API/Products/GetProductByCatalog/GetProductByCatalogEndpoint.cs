using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProductByCatalog;

public sealed record GetProductByCatalogRequest([FromQuery(Name ="catalogs")] string[] Catalogs);
public sealed record GetProductByCatalogResponse(IReadOnlyList<Product> Products);
public sealed class GetProductByCatalogEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/by-catalog", async (ISender sender, [AsParameters] GetProductByCatalogRequest request) =>
        {
            var catalogs = request.Catalogs
                        .Where(c => !string.IsNullOrWhiteSpace(c))
                        .Select(c => c.Trim())
                        .ToList();
            var result = await sender.Send(new GetProductByCatalogQuery(catalogs));
            return Results.Ok(new GetProductByCatalogResponse(result.Products));
        })
.WithName("GetProductByCatalog")
.Produces<GetProductByCatalogResponse>(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status400BadRequest)
.WithSummary("Get products by catalog")
.WithDescription("Get products by catalog");


    }
}
