using CouponAPI.Model;
using CouponAPI.Model.DTOs;

namespace CouponAPI.Mapper
{
    public static class CouponMapper
    {
        public static Coupon ToCouponFromCouponDto(this CouponDto couponDto)
        {
            return new Coupon
            {
                Id = couponDto.Id,
                DiscountAmount = couponDto.DiscountAmount,
                CouponCode = couponDto.CouponCode,
                MinAmount = couponDto.MinAmount
            };
        }

        public static CouponDto ToCouponDtoFromCoupon(this Coupon coupon)
        {
            return new CouponDto
            {
                Id = coupon.Id,
                DiscountAmount = coupon.DiscountAmount,
                CouponCode = coupon.CouponCode,
                MinAmount = coupon.MinAmount
            };
        }
    }
}
