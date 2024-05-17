using graffino_online_store_api.System.Constants;
using graffino_online_store_api.Users.Controllers;
using graffino_online_store_api.Users.DTOs;
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

    public override async Task<ActionResult<GetUserDetailsResponse>> GetUserDetails()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound(ExceptionMessages.USER_DOES_NOT_EXIST);
        }

        IEnumerable<string> roles = await userManager.GetRolesAsync(user);
        GetUserDetailsResponse response = new GetUserDetailsResponse
        {
            Id = user.Id,
            Name = user.UserName!,
            Email = user.Email!,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumber = user.PhoneNumber!,
            Roles = roles
        };
        return response;
    }
}