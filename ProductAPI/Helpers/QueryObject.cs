namespace ProductAPI.Helpers
{
    public class QueryObject
    {
        public string? CategoryName { get; set; }
        public int? CategoryId {  get; set; }
        public double MinPrice { get; set; } = 0;
        public double MaxPrice { get; set; } = double.MaxValue;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
