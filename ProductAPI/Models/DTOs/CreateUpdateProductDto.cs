namespace ProductAPI.Models.DTOs
{
    public class CreateUpdateProductDto
    {
        public required string Name { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public int CategoryId { get; set; }
    }
}
