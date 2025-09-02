namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base()
    {
    }
    public ProductNotFoundException(string message) : base(message)
    {
    }
}
