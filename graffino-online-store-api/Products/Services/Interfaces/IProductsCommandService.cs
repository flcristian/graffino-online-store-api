using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Models;

namespace graffino_online_store_api.Products.Services.Interfaces;

public interface IProductsCommandService
{
    Task<Category> CreateCategory(CreateCategoryRequest request);
    Task<Product> CreateProduct(CreateProductRequest request);
    Task<Category> UpdateCategory(UpdateCategoryRequest request);
    Task<Product> UpdateProduct(UpdateProductRequest request);
    Task<Category> DeleteCategoryById(int categoryId);
    Task<Product> DeleteProductById(int productId);
}