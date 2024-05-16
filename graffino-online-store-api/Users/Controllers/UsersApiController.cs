using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.Users.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public abstract class UsersApiController : ControllerBase
{
    [HttpGet("roles")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<string>))]
    [ProducesResponseType(statusCode: 404, type: typeof(string))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<string>>> GetUserRoles();
}