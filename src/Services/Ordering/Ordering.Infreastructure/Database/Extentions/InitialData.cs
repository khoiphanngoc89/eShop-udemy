namespace Ordering.Infrastructure.Database.Extentions;

internal static class InitialData
{
    public static IEnumerable<Customer> Customers =>
    [
        Customer.Create(CustomerId.Of(new Guid("601b6197-ce5b-4b36-9f77-828a75b4cdef")), "john smith", "john@example.com"),
        Customer.Create(CustomerId.Of(new Guid("fc28e7c3-bb2a-4c45-938a-3f81b6bd514a")), "tom brush", "tom@example.com")
    ];

    public static IEnumerable<Product> Products =>
    [
        Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "IPhone X", 950.00M),
        Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Samsung 10", 840.00M),
        Product.Create(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Huawei Plus",650.00M),
        Product.Create(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Xiaomi Mi 9", 470.00M)
    ];

    public static IEnumerable<Order> Orders
    {
        get
        {
            var address1 = Address.Of("john", "smith", "john@example.com", "Bahcelievler No:4", "Turkey", "Istanbul", "38050");
            var address2 = Address.Of("tom", "brush", "jtom@example.com", "Broadway No:1", "England", "Nottingham", "08050");

            var payment1 = Payment.Of("mehmet", "5555555555554444", "12/28", "355", "1");
            var payment2 = Payment.Of("john", "8885555555554444", "06/30", "222", "2");

            var order1 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            CustomerId.Of(new Guid("601b6197-ce5b-4b36-9f77-828a75b4cdef")),
                            OrderName.Of("ORD_1"),
                            shippingAddress: address1,
                            billingAddress: address1,
                            payment1);
            order1.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 2, 950.00M);
            order1.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 1, 840.00M);

            var order2 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            CustomerId.Of(new Guid("fc28e7c3-bb2a-4c45-938a-3f81b6bd514a")),
                            OrderName.Of("ORD_2"),
                            shippingAddress: address2,
                            billingAddress: address2,
                            payment2);
            order2.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 1, 950.00M);
            order2.Add(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 2, 650.00M);

            return [order1, order2];
        }
    }

}
