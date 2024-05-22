using graffino_online_store_api.System.Constants;
using graffino_online_store_api.Users.Controllers.Interfaces;
using graffino_online_store_api.Users.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace graffino_online_store_api.Users.Controllers;

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

    public override async Task<ActionResult<string>> ChangePassword(ChangePasswordRequest request)
    {
        if (!request.CurrentPassword.Equals(request.NewPassword))
            return BadRequest("Incorrect old password, please check and try again.");

        IdentityUser? user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return NotFound("User does not exist.");

        await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        return Accepted("Successfully changed password.", "Successfully changed password.");
    }
}