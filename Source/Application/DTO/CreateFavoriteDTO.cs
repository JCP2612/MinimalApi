using ProductsNet.Source.Domain;

namespace ProductsNet.Source.Application.DTO
{
    public class CreateFavoriteDTO
    {
        public required int ProductId { get; set; }
        public required int UserId { get; set; }
    }
}