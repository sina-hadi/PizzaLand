using ShoppingCartAPI.Model.DTOs;

namespace ShoppingCartAPI.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDto> GetCart(string userId);
        Task<ResponseDto> UpsertCart(CartUpsertDto cart, string userId);
        Task<ResponseDto> RemoveCart(string userId);
        Task<ResponseDto> RemoveCartDetails(int cartDetailsId);
        Task<ResponseDto> IncreaseCartDetail(int cartDetailsId);
        Task<ResponseDto> DecreaseCartDetail(int cartDetailsId);
    }
}
