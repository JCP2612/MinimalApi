using ProductsNet.Source.Domain;
using ProductsNet.Source.Infraestructure;

namespace ProductsNet.Source.Application
{
    public class AddProductCase(ProductRepository productRepository, CategoryRepository categoryRepository)
    {
        private readonly ProductRepository _productRepository = productRepository;
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task ExecuteAsync(Product product, List<int> categoryIds)
        {
            if (product.Price <= 0) throw new ArgumentException("El valor del producto debe ser mayor a 0");
            if (string.IsNullOrWhiteSpace(product.Name)) throw new ArgumentException("El nombre del producto no puede ir vacio");
            product.Categories = [];
            if (categoryIds != null && categoryIds.Count > 0)
            {
                foreach (var categoryId in categoryIds)
                {
                    var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                    if (category != null) product.Categories.Add(category);
                }
            }
            await _productRepository.AddProductAsync(product);
        }
    }
}