using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = null!;

    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon {Id = 1,ProductName = "Iphone 17", Description = "IPhone 17 Phone", Amount = 1},
            new Coupon {Id = 2,ProductName = "Samsung S25", Description = "Samsung S25", Amount = 1}
            );
    }
}