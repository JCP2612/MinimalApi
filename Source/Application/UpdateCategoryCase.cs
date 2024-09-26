using ProductsNet.Source.Application.DTO;
using ProductsNet.Source.Infraestructure;
using ProductsNet.Source.Domain;

namespace ProductsNet.Source.Application
{
    public class UpdateCategoryUseCase(CategoryRepository categoryRepository)
    {
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task<Category> ExecuteAsync(Category category, Category changes)
        {
            if (!string.IsNullOrWhiteSpace(category.Name)) category.Name = changes.Name;

            await _categoryRepository.UpdateCategoryAsync(category);
            return category;
        }
    }
}