using Microsoft.EntityFrameworkCore;
using ProductsNet.Source.Domain;
using ProductsNet.Source.Infraestructure.Data;

namespace ProductsNet.Source.Infraestructure
{
    public class CategoryRepository(ApplicationDBContext context)
    {
        private readonly ApplicationDBContext _context = context;

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}