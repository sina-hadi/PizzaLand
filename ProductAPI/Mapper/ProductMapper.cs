using ProductAPI.Models;
using ProductAPI.Models.DTOs;

namespace ProductAPI.Mapper
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDtoFromProduct(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageLocalPath = product.ImageLocalPath,
                ImageUrl = product.ImageUrl,
                CategoryId  = product.CategoryId,
                CategoryName = product.CategoryName
            };
        }

        public static Product ToProductFromProductDTO(this CreateUpdateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                ImageLocalPath = productDto.ImageLocalPath,
                ImageUrl = productDto.ImageUrl,
                CategoryId = productDto.CategoryId,
            };
        }
    }
}
