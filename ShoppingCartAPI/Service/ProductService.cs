using Newtonsoft.Json;
using ShoppingCartAPI.Model.DTOs;
using ShoppingCartAPI.Service.IService;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(List<int> ids)
        {
            var client = _httpClientFactory.CreateClient("Product");
            var okss = System.Text.Json.JsonSerializer.Serialize(ids);
            HttpContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(ids), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/api/product/getproducts", content);
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<List<ProductDto>>(apiContet);
            return resp;
        }
    }
}
