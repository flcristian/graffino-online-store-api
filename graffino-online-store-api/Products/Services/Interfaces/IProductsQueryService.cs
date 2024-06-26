using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Models;

namespace graffino_online_store_api.Products.Services.Interfaces;

public interface IProductsQueryService
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task<IEnumerable<Product>> GetAllProducts();
    Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId);
    Task<Category> GetCategoryById(int categoryId);
    Task<Product> GetProductById(int productId);
    Task<FilterProductsResponse> FilterProducts(int? categoryId, string? search, Dictionary<string, string> properties, int? page, int? itemsPerPage, string? sort);
    Task<Dictionary<string, List<string>>> GetFilterCriteria(int categoryId);
}