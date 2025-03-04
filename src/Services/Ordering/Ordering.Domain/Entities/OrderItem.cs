namespace Ordering.Domain.Entities;

public class OrderItem : EntityBase<OrderItemId>
{
    internal OrderItem(OrderId OrderId, ProductId ProductId, int Quantity, decimal Price)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        this.OrderId = OrderId;
        this.ProductId = ProductId;
        this.Quantity = Quantity;
        this.Price = Price;
    }

    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    // 1 product can have many orderitem (relationship)
    //
    // can define or not define depend on the requirement need loading Product (include)
    // if need loading Product with OrderItem then in OrderItemConfiguration.cs 
    // the configuration should like this:
    //  builder.HasOne(orderItem => orderItem.Product)
    //        .WithMany()
    //        .HasForeignKey(orderItem => orderItem.ProductId)
    //        .IsRequired();
    //
    // unless, the configuration should like below:
    //  builder.HasOne<Product>()
    //         .WithMany()
    //         .HasForeignKey(orderItem => orderItem.ProductId)
    //
    // public virtual Product Product { get; private set; } = default!;

}
