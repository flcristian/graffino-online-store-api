using graffino_online_store_api.OrderDetails.Controllers.Interfaces;
using graffino_online_store_api.OrderDetails.DTOs;
using graffino_online_store_api.OrderDetails.Models;
using graffino_online_store_api.OrderDetails.Services.Interfaces;
using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.Orders.Services.Interfaces;
using graffino_online_store_api.System.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.OrderDetails.Controllers;

public class OrderDetailsController(
    ILogger<OrderDetailsController> logger,
    IOrdersQueryService ordersQueryService,
    UserManager<IdentityUser> userManager,
    IOrderDetailsQueryService queryService,
    IOrderDetailsCommandService commandService
    ) : OrderDetailsApiController
{
    #region QUERY ENDPOINTS
    
    public override async Task<ActionResult<IEnumerable<OrderDetail>>> GetAllOrderDetails()
    {
        logger.LogInformation("GET Rest Request: Get all order details.");
        
        try
        {
            IEnumerable<OrderDetail> orderDetails = await queryService.GetAllOrderDetails();

            return Ok(orderDetails);
        }
        catch (ItemsDoNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }

    public override async Task<ActionResult<OrderDetail>> GetOrderDetailById(int id)
    {
        logger.LogInformation("GET Rest Request: Get order detail by ID {Id}.", id);

        try
        {
            OrderDetail orderDetail = await queryService.GetOrderDetailById(id);

            return Ok(orderDetail);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }
    
    #endregion
    
    #region COMMAND ENDPOINTS

    public override async Task<ActionResult<OrderDetail>> CreateOrderDetail(CreateOrderDetailRequest request)
    {
        logger.LogInformation("POST Rest Request: Create order detail detail.");

        try
        {
            Order order = await ordersQueryService.GetOrderById(request.OrderId);
            
            var user = await userManager.GetUserAsync(User);
            IEnumerable<string> roles = await userManager.GetRolesAsync(user!);
            if (!order.CustomerId.Equals(user!.Id) && !roles.Contains("Administrator"))
            {
                return Unauthorized("Invalid operation.");
            }
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
        
        try
        {
            OrderDetail orderDetail = await commandService.CreateOrderDetail(request);

            return Created(GenerateUriForOrderDetail(orderDetail.Id), orderDetail);
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
        catch (ItemAlreadyExistsException exception)
        {
            logger.LogInformation(exception, $"400 Rest Response: {exception.Message}");
            return BadRequest(exception.Message);
        }
    }

    public override async Task<ActionResult<OrderDetail>> UpdateOrderDetail(UpdateOrderDetailRequest request)
    {
        logger.LogInformation("PUT Rest Request: Update order detail with ID {Id}.", request.Id);

        logger.LogInformation("POST Rest Request: Create order detail detail.");

        try
        {
            OrderDetail od = await queryService.GetOrderDetailById(request.Id);
            Order order = await ordersQueryService.GetOrderById(od.OrderId);
            
            var user = await userManager.GetUserAsync(User);
            IEnumerable<string> roles = await userManager.GetRolesAsync(user!);
            if (!order.CustomerId.Equals(user!.Id) && !roles.Contains("Administrator"))
            {
                return Unauthorized("Invalid operation.");
            }
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
        
        try
        {
            OrderDetail orderDetail = await commandService.UpdateOrderDetail(request);

            return Created(GenerateUriForOrderDetail(orderDetail.Id), orderDetail);
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

    public override async Task<ActionResult<OrderDetail>> DeleteOrderDetailById(int id)
    {
        logger.LogInformation("DELETE Rest Request: Delete order detail with ID {Id}.", id);
        
        try
        {
            OrderDetail orderDetail = await commandService.DeleteOrderDetailById(id);

            return Created(GenerateUriForOrderDetail(orderDetail.Id), orderDetail);
        }
        catch (ItemDoesNotExistException exception)
        {
            logger.LogInformation(exception, $"404 Rest Response: {exception.Message}");
            return NotFound(exception.Message);
        }
    }
    
    #endregion
    
    #region PRIVATE METHODS
    
    private string GenerateUriForOrderDetail(int orderDetailId)
    {
        return Url.Action("GetOrderDetailById", "OrderDetails", new { id = orderDetailId }, Request.Scheme)!;
    }
    
    #endregion
}