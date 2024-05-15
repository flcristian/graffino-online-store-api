using graffino_online_store_api.Products.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace graffino_online_store_api.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{   
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Clothing> Clothing { get; set; }
    public virtual DbSet<TV> Televisions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().UseTptMappingStrategy();
    }
}