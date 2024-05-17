using graffino_online_store_api.Products.Controllers.Interfaces;
using graffino_online_store_api.Products.DTOs;
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
    
    public override async Task<ActionResult<Dictionary<string, IEnumerable<object>>>> GetAllProducts()
    {
        logger.LogInformation("GET Rest Request: Get all products.");

        try
        {
            Dictionary<string, IEnumerable<object>> products = await queryService.GetAllProducts();

            return Ok(products);
        }
        catch (ItemsDoNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<GetClothingResponse>>> GetAllClothing()
    {
        logger.LogInformation("GET Rest Request: Get all clothing.");

        try
        {
            IEnumerable<GetClothingResponse> clothing = await queryService.GetAllClothing();

            return Ok(clothing);
        }
        catch (ItemsDoNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<GetTVResponse>>> GetAllTelevisions()
    {
        logger.LogInformation("GET Rest Request: Get all televisions.");

        try
        {
            IEnumerable<GetTVResponse> televisions = await queryService.GetAllTelevisions();

            return Ok(televisions);
        }
        catch (ItemsDoNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<GetClothingResponse>> GetClothingById(int id)
    {
        logger.LogInformation("GET Rest Request: Get clothing with ID {Id}.", id);

        try
        {
            GetClothingResponse clothing = await queryService.GetClothingById(id);

            return Ok(clothing);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<GetTVResponse>> GetTelevisionById(int id)
    {
        logger.LogInformation("GET Rest Request: Get television with ID {Id}.", id);

        try
        {
            GetTVResponse television = await queryService.GetTVById(id);

            return Ok(television);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    #endregion
    
    #region COMMAND ENDPOINTS
    
    public override async Task<ActionResult<GetClothingResponse>> CreateClothing(CreateClothingRequest request)
    {
        logger.LogInformation("POST Rest Request: Create clothing.");

        try
        {
            GetClothingResponse clothing = await commandService.CreateClothing(request);

            return Created(GenerateUriForClothing(clothing.Id), clothing);
        }
        catch (InvalidValueException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }

    public override async Task<ActionResult<GetTVResponse>> CreateTelevision(CreateTVRequest request)
    {
        logger.LogInformation("POST Rest Request: Create television.");

        try
        {
            GetTVResponse television = await commandService.CreateTV(request);

            return Created(GenerateUriForTelevision(television.Id), television);
        }
        catch (InvalidValueException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }

    public override async Task<ActionResult<GetClothingResponse>> UpdateClothing(UpdateClothingRequest request)
    {
        logger.LogInformation("PUT Rest Request: Update clothing with ID {Id}.", request.Id);

        try
        {
            GetClothingResponse clothing = await commandService.UpdateClothing(request);

            return Accepted(GenerateUriForClothing(clothing.Id), clothing);
        }
        catch (InvalidValueException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }

    public override async Task<ActionResult<GetTVResponse>> UpdateTelevision(UpdateTVRequest request)
    {
        logger.LogInformation("PUT Rest Request: Update television with ID {Id}.", request.Id);

        try
        {
            GetTVResponse television = await commandService.UpdateTV(request);

            return Accepted(GenerateUriForClothing(television.Id), television);
        }
        catch (InvalidValueException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }

    public override async Task<ActionResult<GetClothingResponse>> DeleteClothing(int id)
    {
        logger.LogInformation("DELETE Rest Request: Delete clothing with ID {Id}.", id);

        try
        {
            GetClothingResponse clothing = await commandService.DeleteClothingById(id);

            return Accepted(GenerateUriForClothing(clothing.Id), clothing);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }

    public override async Task<ActionResult<GetTVResponse>> DeleteTelevision(int id)
    {
        logger.LogInformation("DELETE Rest Request: Delete television with ID {Id}.", id);

        try
        {
            GetTVResponse television = await commandService.DeleteTVById(id);

            return Accepted(GenerateUriForClothing(television.Id), television);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }
    
    #endregion

    #region PRIVATE METHODS

    private string GenerateUriForClothing(int clothingId)
    {
        return Url.Action("GetClothingById", "Products", new { id = clothingId }, Request.Scheme)!;
    }
    
    private string GenerateUriForTelevision(int televisionId)
    {
        return Url.Action("GetTelevisionById", "Products", new { id = televisionId }, Request.Scheme)!;
    }

    #endregion
}