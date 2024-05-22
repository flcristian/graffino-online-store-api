using graffino_online_store_api.OrderDetails.DTOs;
using graffino_online_store_api.OrderDetails.Models;
using graffino_online_store_api.System.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.OrderDetails.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class OrderDetailsApiController : ControllerBase
{
    #region QUERY ENDPOINTS

    [HttpGet("all-order-details")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<OrderDetail>))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<OrderDetail>>> GetAllOrderDetails();
    
    [HttpGet("order-detail/{id}")]
    [ProducesResponseType(statusCode: 200, type: typeof(OrderDetail))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<OrderDetail>> GetOrderDetailById([FromRoute] int id);
    
    #endregion

    #region COMMAND ENDPOINTS

    [HttpPost("create")]
    [ProducesResponseType(statusCode: 201, type: typeof(OrderDetail))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<OrderDetail>> CreateOrderDetail([FromBody] CreateOrderDetailRequest request);

    [HttpPut("update"), Authorize]
    [ProducesResponseType(statusCode: 202, type: typeof(OrderDetail))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<OrderDetail>> UpdateOrderDetail([FromBody] UpdateOrderDetailRequest request);

    [HttpDelete("delete/{id}"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 202, type: typeof(OrderDetail))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<OrderDetail>> DeleteOrderDetailById([FromRoute] int id);
    
    #endregion
}