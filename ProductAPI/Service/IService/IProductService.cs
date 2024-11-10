using ProductAPI.Helpers;
using ProductAPI.Models;

namespace ProductAPI.Service.IService
{
    public interface IProductService
    {
        Task<List<Product?>> GetProductsAsync(QueryObject query);
        Task<Product?> GetProductById(int id);
        Task<List<Product?>> GetProductByIds(List<int> ids);
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<Coupon> GetCoupon(int id);
    }
}
