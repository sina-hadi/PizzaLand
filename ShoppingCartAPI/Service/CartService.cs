using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Mapper;
using ShoppingCartAPI.Model;
using ShoppingCartAPI.Model.DTOs;
using ShoppingCartAPI.Service.IService;

namespace ShoppingCartAPI.Service
{
    public class CartService : ICartService
    {
        private ResponseDto response;
        private IProductService _productService;
        private readonly AppDbContext _db;

        public CartService(IProductService productService, AppDbContext db)
        {
            response = new ResponseDto();
            _productService = productService;
            _db = db;
        }

        public async Task<ResponseDto> GetCart(string userId)
        {
            CartDto cart = new()
            {
                CartHeader = _db.CartHeaders.First(u => u.UserId == userId).ToCartHeaderDtoFromCartHeader()
            };
            cart.CartDetails = _db.CartDetails.Where(u => u.CartHeaderId == cart.CartHeader.Id).Select(c => c.ToCartDetailsDtoFromCartDetails());

            var listOfProductsIds = new List<int>();

            foreach(CartDetailsDto item in cart.CartDetails)
            {
                listOfProductsIds.Add(item.ProductId);
            }

            IEnumerable<ProductDto> products = await _productService.GetProducts(listOfProductsIds);

            IList<CartDetailsDto> newList = [];

            foreach (var item in cart.CartDetails)
            {
                item.Product = products.FirstOrDefault(u => u.Id == item.ProductId);
                newList.Add(item);
                cart.CartHeader.CartTotal += (double)(item.Count * item.Product.Price);
            }

            cart.CartDetails = newList.AsEnumerable();

            response.IsSuccess = true;
            response.Result = cart;

            return response;
        }

        public async Task<ResponseDto> RemoveCart(string userId)
        {
            CartHeader? cart = _db.CartHeaders.FirstOrDefault(u => u.UserId == userId);
            if (cart == null)
            {
                response.IsSuccess = false;
                response.Message = "There is no cart available for this user";
                return response;
            }
            _db.CartHeaders.Remove(cart);
            await _db.SaveChangesAsync();
            response.IsSuccess = true;
            return response;
        }

        public async Task<ResponseDto> RemoveCartDetails(int cartDetailsId)
        {
            CartDetail? cart = _db.CartDetails.FirstOrDefault(u => u.Id == cartDetailsId);
            if (cart == null)
            {
                response.IsSuccess = false;
                response.Message = "There is no cart detail available with this id";
                return response;
            }
            _db.CartDetails.Remove(cart);
            await _db.SaveChangesAsync();
            response.IsSuccess = true;
            return response;
        }

        public async Task<ResponseDto> UpsertCart(CartUpsertDto cart, string userId)
        {
            CartHeader? cartHeader = _db.CartHeaders.FirstOrDefault(u => u.UserId == userId);
            if (cartHeader == null)
            {
                cartHeader = new CartHeader
                {
                    UserId = userId
                };
                await _db.CartHeaders.AddAsync(cartHeader);
                await _db.SaveChangesAsync();
            }

            CartDetail cartDetail = new CartDetail
            {
                CartHeaderId =  cartHeader.Id,
                ProductId = cart.ProductId,
                Count = cart.Count
            };
            await _db.CartDetails.AddAsync(cartDetail);
            await _db.SaveChangesAsync();
            response.IsSuccess = true;
            return response;
        }

        public async Task<ResponseDto> IncreaseCartDetail(int cartDetailsId)
        {
            CartDetail? cart = _db.CartDetails.FirstOrDefault(u => u.Id == cartDetailsId);
            if (cart == null)
            {
                response.IsSuccess = false;
                response.Message = "There is no cart detail available with this id";
                return response;
            }
            cart.Count += 1;
            await _db.SaveChangesAsync();
            response.IsSuccess = true;
            return response;
        }

        public async Task<ResponseDto> DecreaseCartDetail(int cartDetailsId)
        {
            CartDetail? cart = _db.CartDetails.FirstOrDefault(u => u.Id == cartDetailsId);
            if (cart == null)
            {
                response.IsSuccess = false;
                response.Message = "There is no cart detail available with this id";
                return response;
            }
            if (cart.Count == 1)
            {
                await RemoveCartDetails(cartDetailsId);
            } else
            {
                cart.Count -= 1;
            }
            await _db.SaveChangesAsync();
            response.IsSuccess = true;
            return response;
        }
    }
}
