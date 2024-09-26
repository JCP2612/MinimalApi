
using ProductsNet.Source.Domain;
using ProductsNet.Source.Infraestructure;

namespace ProductsNet.Source.Application
{
    public class DeleteFavoriteUseCase(FavoritesRepository favoritesRepository)
    {
        private readonly FavoritesRepository _favoritesRepository = favoritesRepository;

        public async Task<bool> ExecuteAsync(Product product, User user)
        {
            var favorite = await _favoritesRepository.FindOneByAsync(user, product);
            if (favorite != null)
            {
                await _favoritesRepository.DeleteFavoriteAsync(favorite.Id);
                return true;
            }
            return false;
        }
    }
}