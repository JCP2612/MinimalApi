using ProductsNet.Source.Domain;

namespace ProductsNet.Source.Application.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Boolean IsFavorite { get; set; } = false;
        public int Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<Category> Categories { get; set; } = [];

    }
}