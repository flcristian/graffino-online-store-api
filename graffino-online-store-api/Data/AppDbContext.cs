using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace graffino_online_store_api.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{   
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}