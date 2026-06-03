namespace Ordering.Domain.Models;

public class OrderItem :Entity<OrderItem>
{
    internal OrderItem(OrderId orderId,ProductId productId, int quantity,decimal price)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
    public OrderId OrderId { get; private set; } = Guid.Empty;
    public ProductId ProductId { get; private set; } = Guid.Empty;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}