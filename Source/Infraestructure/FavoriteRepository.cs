using Microsoft.EntityFrameworkCore;
using ProductsNet.Source.Domain;
using ProductsNet.Source.Infraestructure.Data;


namespace ProductsNet.Source.Infraestructure
{
    public class FavoritesRepository(ApplicationDBContext context)
    {
        private readonly ApplicationDBContext _context = context;

        public async Task AddFavorite(Favorites favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<Favorites?> GetFavoriteByIdAsync(int id)
        {
            return await _context.Favorites
            .Include(u => u.User)
            .Include(p => p.Product)
            .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<Favorites>> GetAllFavoriteByUserAsync(User user)
        {
            return await _context.Favorites
            .Include(u => u.User)
            .Include(p => p.Product)
            .Where(u => u.User.Id == user.Id)
            .ToListAsync();
        }
        public async Task<Favorites?> FindOneByAsync(User user, Product product)
        {
            return await _context.Favorites
            .Include(u => u.User)
            .Include(p => p.Product)
            .Where(u => u.User.Id == user.Id)
            .Where(p => p.Product.Id == product.Id)
            .FirstOrDefaultAsync();
        }

        public async Task DeleteFavoriteAsync(int id)
        {
            var fav = await _context.Favorites.FindAsync(id);
            if (fav != null)
            {
                _context.Favorites.Remove(fav);
                await _context.SaveChangesAsync();
            }
        }
    }
}