using ProductsNet.Source.Application.DTO;
using ProductsNet.Source.Domain;
using ProductsNet.Source.Infraestructure;

namespace ProductsNet.Source.Application
{
    public class AddFavoriteUseCase(FavoritesRepository favoritesRepository)
    {
        private readonly FavoritesRepository _favoritesRepository = favoritesRepository;

        public async Task<FavoriteResponseDTO?> ExecuteAsync(Product product, User user)
        {
            var favoriteOld = await _favoritesRepository.FindOneByAsync(user, product);
            if (favoriteOld == null)
            {
                var Favorites = new Favorites
                {
                    Product = product,
                    User = user,
                    DateAdd = DateTime.UtcNow
                };

                await _favoritesRepository.AddFavorite(Favorites);
                return new FavoriteResponseDTO
                {
                    Product = product,
                    DateAdd = Favorites.DateAdd,
                    Id = Favorites.Id
                };
            }
            else
                return new FavoriteResponseDTO
                {
                    Product = favoriteOld.Product,
                    Id = favoriteOld.Id,
                    DateAdd = favoriteOld.DateAdd
                };
        }
    }
}