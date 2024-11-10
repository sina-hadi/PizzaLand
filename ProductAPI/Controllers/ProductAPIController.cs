using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductAPI.Helpers;
using ProductAPI.Mapper;
using ProductAPI.Models;
using ProductAPI.Models.DTOs;
using ProductAPI.Service.IService;

namespace ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductService _productService;
        private bool successfulRequest;
        private ProductDto productDto;

        public ProductAPIController(IProductService productService)
        {
            _productService = productService;
            successfulRequest = true;
            productDto = new ProductDto() { Name = "Empty" };
        }

        [HttpGet("{id}")]
        [ProducesResponseType<ProductDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Product? product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            productDto = product.ToProductDtoFromProduct();

            return Ok(productDto);
        }

        [HttpGet]
        [ProducesResponseType<List<ProductDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<ProductDto> productDto = new();

            List<Product?> product = await _productService.GetProductsAsync(query);
            if (product.IsNullOrEmpty())
            {
                return NoContent();
            }
            productDto = product.Select(c => c.ToProductDtoFromProduct()).ToList();

            return Ok(productDto.ToList());
        }

        [HttpPost]
        [ProducesResponseType<ProductDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Product product = productDto.ToProductFromProductDTO();

            successfulRequest = await _productService.AddProductAsync(product);
            if (!successfulRequest)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, productDto);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] CreateUpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Product product = productDto.ToProductFromProductDTO();

            successfulRequest = await _productService.UpdateProductAsync(id, product);
            if (!successfulRequest)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            successfulRequest = await _productService.DeleteProductAsync(id);
            if (!successfulRequest)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // Test to pass a list of ids for get products
        [HttpPost("getproducts")]
        public async Task<IActionResult> GetProductsByIDs([FromBody] List<int> ids)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<Product?> products = await _productService.GetProductByIds(ids);
            if (products.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(products);
        }

        // Test to access to sql tables
        [HttpGet("getcoupon/{id}")]
        public async Task<IActionResult> GetCoupon(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Coupon coupon = await _productService.GetCoupon(id);
            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }
    }
}
