namespace Catalog.API.Products.GetProduct;

public sealed record GetProductsQuery() : IQuery<GetProductsResult>;
public sealed record GetProductsResult(IEnumerable<Product> Products);
public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) :
    IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products with {@Query}", query);
        var products = await session.Query<Product>()
            .ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}
