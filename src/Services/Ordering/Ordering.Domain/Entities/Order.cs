using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public sealed class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity);

    // 1 customer can have many orders (relationship)
    //
    // can define or not define depend on the requirement need loading Customer  (include)
    // if need loading Customer with Order then in OrderConfiguration.cs 
    // the configuration should like this:
    //  builder.HasOne(order => order.Customer)
    //        .WithMany()
    //        .HasForeignKey(order => order.CustomerId)
    //        .IsRequired();
    //
    // unless, the configuration should like below:
    //  builder.HasOne<Customer>()
    //         .WithMany()
    //         .HasForeignKey(order => order.CustomerId)
    //
    // public virtual Customer Customer { get; private set; } = default!;
    public static Order Create(OrderId id,
                               CustomerId customerId,
                               OrderName orderName,
                               Address shippingAddress,
                               Address billingAddress,
                               Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending,
        };

        order.AddEvents(new OrderCreatedDomainEvent(order));
        return order;
    }

    public void Update(OrderName orderName,
                       Address shippingAddress,
                       Address billingAddress,
                       Payment payment,
                       OrderStatus status)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;
        AddEvents(new OrderUpdatedDomainEvent(this));
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        var item = new OrderItem(Id, productId, quantity, price);
        _items.Add(item);
    }

    public void Remove(ProductId productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
        }
    }
}
