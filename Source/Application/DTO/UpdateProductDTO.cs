namespace ProductsNet.Source.Application.DTO
{
    public class UpdateProductDTO
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public List<int> CategoryIds { get; set; } = [];
    }
}