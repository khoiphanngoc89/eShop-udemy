
using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct;

public sealed record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price) :
    ICommand<UpdateProductResult>;
public sealed record UpdateProductResult(bool IsSuccess);
public sealed class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
        ProductNotFoundException.ThrowIfNull(product, $"Product with id {request.Id} not found.");
        product!.Name = request.Name;
        product.Categories = request.Categories;
        product.Description = request.Description;
        product.ImageFile = request.ImageFile;
        product.Price = request.Price;
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);

    }
}
