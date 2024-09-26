using ProductsNet.Source.Application.DTO;
using ProductsNet.Source.Infraestructure;
using ProductsNet.Source.Domain;

namespace ProductsNet.Source.Application
{
    public class UpdateProductUseCase(ProductRepository productRepository, CategoryRepository categoryRepository)
    {
        private readonly ProductRepository _productRepository = productRepository;
        private readonly CategoryRepository _categoryRepository = categoryRepository;

        public async Task<Product> ExecuteAsync(UpdateProductDTO updateProductDTO, Product product)
        {
            if (!string.IsNullOrWhiteSpace(updateProductDTO.Name)) product.Name = updateProductDTO.Name;
            if (!string.IsNullOrWhiteSpace(updateProductDTO.Image)) product.Image = updateProductDTO.Image;
            if (!string.IsNullOrWhiteSpace(updateProductDTO.Description)) product.Description = updateProductDTO.Description;
            if (updateProductDTO.Price != null && int.IsPositive((int)updateProductDTO.Price)) product.Price = (int)updateProductDTO.Price;

            if (updateProductDTO.CategoryIds != null && updateProductDTO.CategoryIds.Count > 0)
            {
                product.Categories = [];
                foreach (var categoryId in updateProductDTO.CategoryIds)
                {
                    var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                    if (category != null) product.Categories.Add(category);
                }
            }

            await _productRepository.UpdateProductAsync(product);
            return product;
        }
    }
}