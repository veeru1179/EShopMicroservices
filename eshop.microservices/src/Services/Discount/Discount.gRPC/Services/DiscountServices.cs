using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Discount.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountServices(DiscountContext dbContext, ILogger<DiscountServices> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupones.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            logger.LogInformation("Discount is retrived for ProductName :{produtName},Amount: {amount}", coupon.ProductName, coupon.Amount);
            if (coupon is null)
            {
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount description" };
            }
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
             var coupon= request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
            }
              dbContext.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount coupon created successfully for product : {ProductName}", coupon.ProductName);
             var couponModel= coupon.Adapt<CouponModel>(); 
            return couponModel;
        }
        public override  async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
            }
            dbContext.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount coupon Updated successfully for product : {ProductName}", coupon.ProductName);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;

        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon= await dbContext.Coupones.FirstOrDefaultAsync(x=>x.ProductName== request.ProductName);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Not Found"));

            dbContext.Remove(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount coupon Deleted successfully for product : {ProductName}", coupon.ProductName);
            return new DeleteDiscountResponse{Success=true};
        }
    }
}
