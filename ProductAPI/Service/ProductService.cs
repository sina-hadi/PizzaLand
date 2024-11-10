using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Helpers;
using ProductAPI.Models;
using ProductAPI.Service.IService;

namespace ProductAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                Product? product = await _db.Products.FindAsync(id);
                if (product == null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<Coupon> GetCoupon(int id)
        {
            Coupon coupon = await _db.Coupon.FirstOrDefaultAsync(c => c.Id == id);
            return coupon;
        }

        public async Task<Product?> GetProductById(int id)
        {
            Product? product = await _db.Products.Include(c => c.CategoryName).FirstOrDefaultAsync(c => c.Id == id);
            return product;
        }

        public async Task<List<Product?>> GetProductByIds(List<int> ids)
        {
            List<Product?> product = await _db.Products.Include(c => c.CategoryName).Where(c => ids.Contains(c.Id)).ToListAsync();
            return product;
        }

        public async Task<List<Product?>> GetProductsAsync(QueryObject query)
        {
            return new List<Product?>();
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            try
            {
                Product? newProduct = await _db.Products.FindAsync(id);

                if (newProduct == null)
                {
                    return false;
                }

                newProduct.Name = product.Name;
                newProduct.Description = product.Description;
                newProduct.ImageLocalPath = product.ImageLocalPath;
                newProduct.ImageUrl = product.ImageUrl;
                newProduct.Price = product.Price;
                newProduct.CategoryId = product.CategoryId;

                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}
