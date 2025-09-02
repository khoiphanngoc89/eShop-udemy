using BuildingBlocks.Common.Cqrs;
using Catalog.API.Exceptions;
using Marten;

namespace Catalog.API.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public sealed record GetProductByIdResult(Product Product);

public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await session.LoadAsync<Product>(query.Id, cancellationToken);
        if (result is null)
        {
            throw new ProductNotFoundException($"Product with id {query.Id} is not found");
        }
        return new GetProductByIdResult(result!);
    }
}
