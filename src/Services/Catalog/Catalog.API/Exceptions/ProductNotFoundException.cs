using BuildingBlocks.Common.Exceptions;
using BuildingBlocks.Common.Extensions;

namespace Catalog.API.Exceptions;

public sealed class ProductNotFoundException(string message) : NotFoundException(message)
{
    private const string ProductNotFoundMessage = "Product is not found.";

    public static void ThrowIfNullOrEmpty(IReadOnlyCollection<Product>? obj, string message = ProductNotFoundMessage)
    {
        if (obj.IsNullOrEmpty())
        {
            throw new ProductNotFoundException(message);
        }
    }
}
