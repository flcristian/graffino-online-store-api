using graffino_online_store_api.Users.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.Users.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class UsersApiController : ControllerBase
{
    [HttpGet("roles"), Authorize]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<string>))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<string>>> GetUserRoles();
    
    [HttpGet("details"), Authorize]
    [ProducesResponseType(statusCode: 200, type: typeof(GetUserDetailsResponse))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetUserDetailsResponse>> GetUserDetails();

    [HttpPost("change-password")]
    [ProducesResponseType(statusCode: 202, type: typeof(GetUserDetailsResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(string))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<string>> ChangePassword(ChangePasswordRequest request);
}