using graffino_online_store_api.System.Constants;
using graffino_online_store_api.Users.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.Users;

public class UsersController(
    UserManager<IdentityUser> userManager
    ) : UsersApiController
{
    public override async Task<ActionResult<IEnumerable<string>>> GetUserRoles()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound(ExceptionMessages.USER_DOES_NOT_EXIST);
        }

        IEnumerable<string> roles = await userManager.GetRolesAsync(user);
        return Ok(roles);
    }
}