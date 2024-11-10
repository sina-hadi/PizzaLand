using ShoppingCartAPI.Model;
using ShoppingCartAPI.Model.DTOs;

namespace ShoppingCartAPI.Mapper
{
    public static class CartMapper
    {
        public static CartHeaderDto ToCartHeaderDtoFromCartHeader(this CartHeader cartHeader)
        {
            return new CartHeaderDto
            {
                Id = cartHeader.Id,
                UserId = cartHeader.UserId,
                CouponCode = cartHeader.CouponCode,
                CartTotal = cartHeader.CartTotal,
                Discount = cartHeader.Discount
            };
        }
        public static CartHeader ToCartHeaderFromCartHeaderDto(this CartHeaderDto cartHeaderDto)
        {
            return new CartHeader
            {
                Id = cartHeaderDto.Id,
                UserId = cartHeaderDto.UserId,
                CouponCode = cartHeaderDto.CouponCode,
                CartTotal = cartHeaderDto.CartTotal,
                Discount = cartHeaderDto.Discount
            };
        }
        public static CartDetailsDto ToCartDetailsDtoFromCartDetails(this CartDetail cartDetail)
        {
            return new CartDetailsDto
            {
                Id = cartDetail.Id,
                CartHeaderId = cartDetail.CartHeaderId,
                ProductId = cartDetail.ProductId,
                Product = cartDetail.Product,
                Count = cartDetail.Count
            };
        }
        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }
            yield return value;
        }
    }
}
