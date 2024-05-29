using System.Collections;
using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Models;

namespace graffino_online_store_api.Products.Repository.Interfaces;

public interface IProductsRepository
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
    Task<Category?> GetCategoryByIdAsync(int categoryId);
    Task<Category?> GetCategoryByNameAsync(string categoryName);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Category> CreateCategoryAsync(CreateCategoryRequest request);
    Task<Product> CreateProductAsync(CreateProductRequest request);
    Task<Category> UpdateCategoryAsync(UpdateCategoryRequest request);
    Task<Product> UpdateProductAsync(UpdateProductRequest request);
    Task<Category> DeleteCategoryByIdAsync(int categoryId);
    Task<Product> DeleteProductByIdAsync(int productId);
    Task<IEnumerable<Product>> FilterProducts(int? categoryId, string? search, Dictionary<string, string> properties, int? page, int? itemsPerPage, string? sort);
}