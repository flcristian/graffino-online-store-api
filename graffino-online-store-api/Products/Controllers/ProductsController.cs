using graffino_online_store_api.Products.Controllers.Interfaces;
using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Models;
using graffino_online_store_api.Products.Services.Interfaces;
using graffino_online_store_api.System.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.Products.Controllers;

public class ProductsController(
    ILogger<ProductsController> logger,
    IProductsQueryService queryService,
    IProductsCommandService commandService
) : ProductsApiController
{
    #region QUERY ENDPOINTS

    public override async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        logger.LogInformation("GET Rest Request: Get all categories.");

        try
        {
            IEnumerable<Category> categories = await queryService.GetAllCategories();

            return Ok(categories);
        }
        catch (ItemsDoNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        logger.LogInformation("GET Rest Request: Get all products.");

        try
        {
            IEnumerable<Product> products = await queryService.GetAllProducts();

            return Ok(products);
        }
        catch (ItemsDoNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int categoryId)
    {
        logger.LogInformation("GET Rest Request: Get products by category ID {Id}.", categoryId);

        try
        {
            IEnumerable<Product> products = await queryService.GetProductsByCategoryId(categoryId);

            return Ok(products);
        }
        catch (ItemsDoNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<Category>> GetCategoryById(int categoryId)
    {
        logger.LogInformation("GET Rest Request: Get category by ID {Id}.", categoryId);

        try
        {
            Category category = await queryService.GetCategoryById(categoryId);

            return Ok(category);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<Product>> GetProductById(int productId)
    {
        logger.LogInformation("GET Rest Request: Get product by ID {Id}.", productId);

        try
        {
            Product product = await queryService.GetProductById(productId);

            return Ok(product);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<Product>>> FilterProducts(int? categoryId, string? search, Dictionary<string, string> properties, int? page, int? itemsPerPage)
    {
        logger.LogInformation("GET Rest Request: Filter products with categoryId: {CategoryId}, search: {Search}, properties: {Properties}.",
            categoryId, search, properties);

        try
        {
            IEnumerable<Product> products =
                await queryService.FilterProducts(categoryId, search, properties, page, itemsPerPage);

            return Ok(products);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
        catch (InvalidValueException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
        catch (ItemsDoNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<Dictionary<string, List<string>>>> FilterCriteria(int categoryId)
    {
        logger.LogInformation("GET Rest Request: Get filter criteria for categoryId: {CategoryId}", categoryId);

        try
        {
            Dictionary<string, List<string>> filterCriteria = await queryService.GetFilterCriteria(categoryId);

            return Ok(filterCriteria);
        }
        catch (ItemsDoNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    #endregion

    #region COMMAND ENDPOINTS

    public override async Task<ActionResult<Category>> CreateCategory(CreateCategoryRequest request)
    {
        logger.LogInformation("POST Rest Request: Create category.");

        try
        {
            Category category = await commandService.CreateCategory(request);

            return Created("Created", category);
        }
        catch (ItemAlreadyExistsException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }

    public override async Task<ActionResult<Product>> CreateProduct(CreateProductRequest request)
    {
        logger.LogInformation("POST Rest Request: Create product.");

        try
        {
            Product product = await commandService.CreateProduct(request);

            return Created("Created", product);
        }
        catch (InvalidValueException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }

    public override async Task<ActionResult<Category>> UpdateCategory(UpdateCategoryRequest request)
    {
        logger.LogInformation("PUT Rest Request: Update category with ID {Id}.", request.Id);

        try
        {
            Category category = await commandService.UpdateCategory(request);

            return Accepted("Updated", category);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<Product>> UpdateProduct(UpdateProductRequest request)
    {
        logger.LogInformation("PUT Rest Request: Update product with ID {Id}.", request.Id);

        try
        {
            Product product = await commandService.UpdateProduct(request);

            return Accepted("Updated", product);
        }
        catch (InvalidValueException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<Category>> DeleteCategoryById(int categoryId)
    {
        logger.LogInformation("DELETE Rest Request: Delete category with ID {Id}.", categoryId);

        try
        {
            Category category = await commandService.DeleteCategoryById(categoryId);

            return Accepted("Deleted", category);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<Product>> DeleteProductById(int productId)
    {
        logger.LogInformation("DELETE Rest Request: Delete product with ID {Id}.", productId);

        try
        {
            Product product = await commandService.DeleteProductById(productId);

            return Accepted("Deleted", product);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    #endregion
}