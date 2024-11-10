using CouponAPI.Data;
using CouponAPI.Model;
using CouponAPI.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CouponAPI.Mapper;

namespace CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;

        public CouponAPIController(AppDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = objList.Select(u => u.ToCouponDtoFromCoupon());
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.Id == id);
                _response.Result = obj.ToCouponDtoFromCoupon();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Result = obj.ToCouponDtoFromCoupon();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = couponDto.ToCouponFromCouponDto();
                _db.Coupons.Add(obj);
                _db.SaveChanges();

                //var options = new Stripe.CouponCreateOptions
                //{
                //    AmountOff = (long)(couponDto.DiscountAmount * 100),
                //    Name = couponDto.CouponCode,
                //    Currency = "usd",
                //    Id = couponDto.CouponCode,
                //};
                //var service = new Stripe.CouponService();
                //service.Create(options);

                _response.Result = obj.ToCouponDtoFromCoupon();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = couponDto.ToCouponFromCouponDto();
                _db.Coupons.Update(obj);
                _db.SaveChanges();

                _response.Result = obj.ToCouponDtoFromCoupon();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.Id == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();

                //var service = new Stripe.CouponService();
                //service.Delete(obj.CouponCode);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}