﻿namespace Catalog.Api.Products.GetProductsByCategory;

public sealed record GetProductsByCategoryQuery(string Catagory)
    : IQuery<GetProductByCategoryResult>;
public sealed record GetProductByCategoryResult(IEnumerable<Product> Products);

internal sealed class GetProductByCatagoryQueryHandler(IDocumentSession session, ILogger<GetProductByCatagoryQueryHandler> logger)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var entities = await session.Query<Product>()
            .Where(x => x.Categories.Contains(request.Catagory))
            .ToListAsync(cancellationToken);
        return new GetProductByCategoryResult(entities);
    }
}
