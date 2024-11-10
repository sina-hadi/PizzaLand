using System.ComponentModel.DataAnnotations;

namespace CouponAPI.Model
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string CouponCode { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
