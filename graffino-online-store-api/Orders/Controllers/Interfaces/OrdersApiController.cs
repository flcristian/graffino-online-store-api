using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.System.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.Orders.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class OrdersApiController : ControllerBase
{
    #region QUERY ENDPOINTS
    
    [HttpGet("all-orders")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Order>))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<Order>>> GetAllOrders();
    
    
    [HttpGet("orders/{customerId}")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Order>))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<Order>>> GetAllOrdersByCustomerId([FromRoute] string customerId);
    
    [HttpGet("cart/{customerId}")]
    [ProducesResponseType(statusCode: 200, type: typeof(Order))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Order>> GetCartByCustomerId([FromRoute] string customerId);
    
    [HttpGet("order/{customerId}")]
    [ProducesResponseType(statusCode: 200, type: typeof(Order))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Order>> GetOrderById([FromRoute] int id);
    
    #endregion
    
    #region COMMAND ENDPOINTS
    
    [HttpPost("create"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 201, type: typeof(Order))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderRequest request);
    
    [HttpPost("update"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 201, type: typeof(Order))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Order>> UpdateOrder([FromBody] UpdateOrderRequest request);
    
    [HttpPost("delete/{id}"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 201, type: typeof(Order))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Order>> DeleteOrderById([FromRoute] int id);
    
    #endregion
}