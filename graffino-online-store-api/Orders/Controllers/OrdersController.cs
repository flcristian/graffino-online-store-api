using graffino_online_store_api.Orders.Controllers.Interfaces;
using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.Orders.Services.Interfaces;
using graffino_online_store_api.System.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.Orders.Controllers;

public class OrdersController(
    ILogger<OrdersController> logger,
    IOrdersQueryService queryService,
    IOrdersCommandService commandService,
    UserManager<IdentityUser> userManager
    ) : OrdersApiController
{
    #region QUERY ENDPOINTS
    
    public override async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        logger.LogInformation("GET Rest Request: Get all orders.");
        
        IEnumerable<Order> orders = await queryService.GetAllOrders();

        return Ok(orders);
    }

    public override async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersByCustomerId(string customerId)
    {
        logger.LogInformation("GET Rest Request: Get all orders by customer ID {Id}.", customerId);

        try
        {
            IEnumerable<Order> orders = await queryService.GetAllOrdersByCustomerId(customerId);

            return Ok(orders);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<Order>> GetOrderById(int id)
    {
        logger.LogInformation("GET Rest Request: Get order by ID {Id}.", id);

        try
        {
            Order order = await queryService.GetOrderById(id);

            return Ok(order);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }
    
    #endregion
    
    #region COMMAND ENDPOINTS

    public override async Task<ActionResult<Order>> CreateOrder(CreateOrderRequest request)
    {
        logger.LogInformation("POST Rest Request: Create cart.");

        try
        {
            Order order = await commandService.CreateOrder(request);

            return Created("Created", order);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
        catch (ItemAlreadyExistsException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }
    
    public override async Task<ActionResult<Order>> UpdateOrder(UpdateOrderRequest request)
    {
        logger.LogInformation("PUT Rest Request: Update order with ID {Id}.", request.Id);

        try
        {
            Order order = await commandService.UpdateOrder(request);

            return Created(GenerateUriForOrder(order.Id), order);
        }
        catch (InvalidValueException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<Order>> DeleteOrderById(int id)
    {
        logger.LogInformation("DELETE Rest Request: Delete order with ID {Id}.", id);
        
        try
        {
            Order order = await commandService.DeleteOrderById(id);

            return Created(GenerateUriForOrder(order.Id), order);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }
    
    #endregion

    #region PRIVATE METHODS

    private string GenerateUriForOrder(int orderId)
    {
        return Url.Action("GetOrderById", "Orders", new { id = orderId }, Request.Scheme)!;
    }

    #endregion
}