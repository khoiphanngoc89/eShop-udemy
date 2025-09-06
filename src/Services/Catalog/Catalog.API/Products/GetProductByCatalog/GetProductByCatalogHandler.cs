using Catalog.API.Exceptions;


namespace Catalog.API.Products.GetProductByCatalog;

public sealed record GetProductByCatalogQuery(List<string> Catalogs) : IQuery<GetProductByCatalogResult>;
public sealed record GetProductByCatalogResult(IReadOnlyList<Product> Products);
public sealed class GetProductByCatalogQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCatalogQuery, GetProductByCatalogResult>
{
    public async Task<GetProductByCatalogResult> Handle(GetProductByCatalogQuery query, CancellationToken cancellationToken)
    {
        var results = await session.Query<Product>()
            .Where(p => p.Categories.Any(c => query.Catalogs.Contains(c)))
            .ToListAsync(cancellationToken);
        ProductNotFoundException.ThrowIfNullOrEmpty(results, $"No products found for catalogs: {string.Join(", ", query.Catalogs)}");
        return new GetProductByCatalogResult(results);
    }    
}
