using System.Collections;
using graffino_online_store_api.Products.Models;
using graffino_online_store_api.Products.Repository.Interfaces;
using graffino_online_store_api.Products.Services.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Exceptions;

namespace graffino_online_store_api.Products.Services;

public class ProductsQueryService(
    IProductsRepository repository
    ) : IProductsQueryService
{
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        List<Category> categories = (await repository.GetAllCategoriesAsync()).ToList();

        if (categories.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.CATEGORIES_DO_NOT_EXIST);
        }

        return categories;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        List<Product> products = (await repository.GetAllProductsAsync()).ToList();

        if (products.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.PRODUCTS_DO_NOT_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
    {
        List<Product> products = (await repository.GetProductsByCategoryIdAsync(categoryId)).ToList();
        
        if (products.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.PRODUCTS_DO_NOT_EXIST);
        }

        return products;
    }

    public async Task<Category?> GetCategoryById(int categoryId)
    {
        Category? category = await repository.GetCategoryByIdAsync(categoryId);

        if (category == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.CATEGORY_DOES_NOT_EXIST);
        }

        return category;
    }

    public async Task<Product?> GetProductById(int productId)
    {
        Product? product = await repository.GetProductByIdAsync(productId);

        if (product == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }

        return product;
    }

    public async Task<IEnumerable<Product>> FilterProducts(int? categoryId, string? search, Dictionary<string, string> properties, int? page, int? itemsPerPage)
    {
        List<Product> products = (await repository.FilterProducts(categoryId, search, properties, page, itemsPerPage)).ToList();
        
        if (products.Count == 0)
        {
            if(!categoryId.HasValue && string.IsNullOrEmpty(search) && properties.Count == 0 && !page.HasValue && !itemsPerPage.HasValue)
            {
                throw new ItemsDoNotExistException(ExceptionMessages.PRODUCTS_DO_NOT_EXIST);

            }

            throw new ItemsDoNotExistException(ExceptionMessages.NO_PRODUCTS_FOR_FILTERS);
        }

        return products;
    }
}