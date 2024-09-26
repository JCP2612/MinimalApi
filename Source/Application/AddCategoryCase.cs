using ProductsNet.Source.Domain;
using ProductsNet.Source.Infraestructure;

namespace ProductsNet.Source.Application
{
    public class AddCategoryUseCase(CategoryRepository categoryRepository)
    {
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task ExecuteAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("La categoria debe incluir un nombre");
            }
            await _categoryRepository.AddCategoryAsync(category);
        }
    }
}