using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Models;
using graffino_online_store_api.Products.Repository.Interfaces;
using graffino_online_store_api.Products.Services.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Exceptions;

namespace graffino_online_store_api.Products.Services;

public class ProductsCommandService(
    IProductsRepository repository
    ) : IProductsCommandService
{
    public async Task<Category> CreateCategory(CreateCategoryRequest request)
    {
        if (await repository.GetCategoryByNameAsync(request.Name) != null)
        {
            throw new ItemAlreadyExistsException(ExceptionMessages.CATEGORY_ALREADY_EXISTS);
        }

        Category category = await repository.CreateCategoryAsync(request);
        return category;
    }

    public async Task<Product> CreateProduct(CreateProductRequest request)
    {
        if (request.Price <= 0)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_PRODUCT_PRICE);
        }
        
        Product product = await repository.CreateProductAsync(request);
        return product;
    }

    public async Task<Category> UpdateCategory(UpdateCategoryRequest request)
    {
        if (await repository.GetCategoryByIdAsync(request.Id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.CATEGORY_DOES_NOT_EXIST);
        }

        if (await repository.GetCategoryByNameAsync(request.Name) != null)
        {
            throw new ItemAlreadyExistsException(ExceptionMessages.CATEGORY_ALREADY_EXISTS);
        }

        Category category = await repository.UpdateCategoryAsync(request);
        return category;
    }

    public async Task<Product> UpdateProduct(UpdateProductRequest request)
    {
        if (request.Price <= 0)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_PRODUCT_PRICE);
        }

        if (await repository.GetProductByIdAsync(request.Id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }

        Product product = await repository.UpdateProductAsync(request);
        return product;
    }

    public async Task<Category> DeleteCategoryById(int categoryId)
    {
        if (await repository.GetCategoryByIdAsync(categoryId) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.CATEGORY_DOES_NOT_EXIST);
        }

        Category category = await repository.DeleteCategoryByIdAsync(categoryId);
        return category;
    }

    public async Task<Product> DeleteProductById(int productId)
    {
        if (await repository.GetProductByIdAsync(productId) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }

        Product product = await repository.DeleteProductByIdAsync(productId);
        return product;
    }
}