namespace CouponAPI.Model.DTOs
{
    public class CouponDto
    {
        public int Id { get; set; }
        public required string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
