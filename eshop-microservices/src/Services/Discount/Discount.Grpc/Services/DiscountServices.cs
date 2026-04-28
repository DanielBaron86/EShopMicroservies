using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountServices(DiscountContext dbContext, ILogger<DiscountServices> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync( c => c.ProductName == request.ProductName );
        if (coupon is null)
        {
            coupon = new Coupon{ ProductName = "No Discount", Amount = 0, Description = "No discount available"};
        }
        logger.LogInformation("Discount for Product Name {productName} , with Amount : {amount}", coupon.ProductName,coupon.Amount);
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null) throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid Coupon Model"));
        dbContext.Add(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Creating coupon with model {coupon}",coupon);
        return coupon.Adapt<CouponModel>();

    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null) throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid Coupon Model"));
        dbContext.Update(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Updating coupon with model {coupon}",coupon);
        return coupon.Adapt<CouponModel>();
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }
}