using ShoppingCartAPI.Model.DTOs;

namespace ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts(List<int> ids);
    }
}
