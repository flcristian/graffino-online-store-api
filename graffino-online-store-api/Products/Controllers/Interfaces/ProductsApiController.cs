using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.System.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.Products.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class ProductsApiController : ControllerBase
{
    #region QUERY ENDPOINTS
    
    [HttpGet("all-products")]
    [ProducesResponseType(statusCode: 200, type: typeof(Dictionary<string, IEnumerable<object>>))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Dictionary<string, IEnumerable<object>>>> GetAllProducts();
    
    [HttpGet("all-clothing")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<GetClothingResponse>))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<GetClothingResponse>>> GetAllClothing();
    
    [HttpGet("all-televisions")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<GetTVResponse>))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<GetTVResponse>>> GetAllTelevisions();

    [HttpGet("clothing/{id}")]
    [ProducesResponseType(statusCode: 200, type: typeof(GetClothingResponse))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetClothingResponse>> GetClothingById([FromRoute] int id);
    
    [HttpGet("television/{id}")]
    [ProducesResponseType(statusCode: 200, type: typeof(GetTVResponse))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetTVResponse>> GetTelevisionById([FromRoute] int id);

    #endregion

    #region COMMAND ENDPOINTS

    [HttpPost("create-clothing"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 201, type: typeof(GetClothingResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetClothingResponse>> CreateClothing([FromBody] CreateClothingRequest request);

    [HttpPost("create-television"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 201, type: typeof(GetTVResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetTVResponse>> CreateTelevision([FromBody] CreateTVRequest request);

    [HttpPut("update-clothing"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 202, type: typeof(GetClothingResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetClothingResponse>> UpdateClothing([FromBody] UpdateClothingRequest request);
    
    [HttpPut("update-television"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 202, type: typeof(GetTVResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetTVResponse>> UpdateTelevision([FromBody] UpdateTVRequest request);

    [HttpDelete("delete-clothing/{id}"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 202, type: typeof(GetClothingResponse))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetClothingResponse>> DeleteClothing([FromRoute] int id);
    
    [HttpDelete("delete-television/{id}"), Authorize(Policy = AuthorizationPolicies.REQUIRE_ADMIN_POLICY)]
    [ProducesResponseType(statusCode: 202, type: typeof(GetTVResponse))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetTVResponse>> DeleteTelevision([FromRoute] int id);

    #endregion
}