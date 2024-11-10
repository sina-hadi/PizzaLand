using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Model.DTOs;
using ShoppingCartAPI.Service.IService;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private ResponseDto response;
        private CartDto cart;

        public CartAPIController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
            response = new ResponseDto();
            cart = new CartDto();
        }

        [HttpGet("getcart/{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            response = await _cartService.GetCart(userId);

            if (!response.IsSuccess) return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPost("UpsertCart/{userId}")]
        public async Task<IActionResult> UpsertCart([FromBody] CartUpsertDto cart,[FromRoute] string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            response = await _cartService.UpsertCart(cart, userId);

            if (!response.IsSuccess) return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpDelete("RemoveCartDetails/{cartDetailsId}")]
        public async Task<IActionResult> RemoveCartDetails(int cartDetailsId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            response = await _cartService.RemoveCartDetails(cartDetailsId);

            if (!response.IsSuccess) return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpDelete("RemoveCart")]
        public async Task<IActionResult> RemoveCart([FromBody] string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            response = await _cartService.RemoveCart(userId);

            if (!response.IsSuccess) return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPut("DecreaseCartDetail/{cartDetailId}")]
        public async Task<IActionResult> DecreaseCartDetails(int cartDetailId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            response = await _cartService.DecreaseCartDetail(cartDetailId);

            if (!response.IsSuccess) return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpPut("IncreaseCartDetail/{cartDetailId}")]
        public async Task<IActionResult> IncreaseCartDetails(int cartDetailId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            response = await _cartService.IncreaseCartDetail(cartDetailId);

            if (!response.IsSuccess) return BadRequest(response.Message);

            return Ok(response);
        }
    }
}
