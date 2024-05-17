using Microsoft.AspNetCore.Identity;

namespace graffino_online_store_api.Users.Repository.Interfaces;

public interface IUsersRepository
{
    Task<IdentityUser?> GetByIdAsync(string id);
}