using graffino_online_store_api.Data;
using graffino_online_store_api.Users.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace graffino_online_store_api.Users.Repository;

public class UsersRepository(AppDbContext context) : IUsersRepository
{
    public async Task<IdentityUser?> GetByIdAsync(string id)
    {
        return await context.Users.FindAsync(id);
    }
}