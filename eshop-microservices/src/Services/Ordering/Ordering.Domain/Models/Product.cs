namespace Ordering.Domain.Models;

public class Product :Entity<ProductId>
{
    public string Name { get; private set; } = String.Empty;
    public decimal Price { get; private set; } = default!;
}