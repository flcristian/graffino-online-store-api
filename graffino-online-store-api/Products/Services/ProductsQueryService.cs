using graffino_online_store_api.Products.DTOs;
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

        return categories;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        List<Product> products = (await repository.GetAllProductsAsync()).ToList();

        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
    {
        List<Product> products = (await repository.GetProductsByCategoryIdAsync(categoryId)).ToList();

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

    public async Task<FilterProductsResponse> FilterProducts(int? categoryId, string? search, Dictionary<string, string> properties, int? page, int? itemsPerPage, string? sort)
    {
        FilterProductsResponse response = (await repository.FilterProducts(categoryId, search, properties, page, itemsPerPage, sort));

        if (categoryId.HasValue && await repository.GetCategoryByIdAsync(categoryId.Value) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.CATEGORY_DOES_NOT_EXIST);
        }

        if (page < 1 || itemsPerPage < 1)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_PAGINATION_PARAMETERS);
        }

        return response;
    }

    public async Task<Dictionary<string, List<string>>> GetFilterCriteria(int categoryId)
    {
        Category? category = await repository.GetCategoryByIdAsync(categoryId);
        if (category == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.CATEGORY_DOES_NOT_EXIST);
        }

        List<Product> products = (await repository.GetProductsByCategoryIdAsync(categoryId)).ToList();
        if (products.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.PRODUCTS_DO_NOT_EXIST);
        }

        Dictionary<string, List<string>> filterCriteria = new Dictionary<string, List<string>>();
        category.Properties.ForEach(property =>
        {
            List<string> propertyValues = new List<string>();

            products.ForEach(product =>
            {
                var productProperty = product.ProductProperties.FirstOrDefault(pp => pp.Property.Name == property.Name);
                if (productProperty != null)
                {
                    propertyValues.Add(productProperty.Value);
                }
            });
            
            List<string> uniquePropertyValues = propertyValues.Distinct().ToList();

            filterCriteria.Add(property.Name, uniquePropertyValues);
        });
        
        return filterCriteria;
    }
}