using ProductsNet.Source.Domain;

namespace ProductsNet.Source.Application.DTO
{
    public class FavoriteResponseDTO
    {
        public int Id { get; set; }
        public required Product Product { get; set; }

        public DateTime DateAdd { get; set; }
    }
}