using graffino_online_store_api.OrderDetails.Models;
using graffino_online_store_api.Orders.Models;
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
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().UseTptMappingStrategy();

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Order>()
            .HasIndex(o => o.CustomerId)
            .HasDatabaseName("IX_Orders_CustomerId");

        modelBuilder.Entity<OrderDetail>()
            .HasIndex(od => od.OrderId)
            .HasDatabaseName("IX_OrderDetails_OrderId");

        modelBuilder.Entity<OrderDetail>()
            .HasIndex(od => od.ProductId)
            .HasDatabaseName("IX_OrderDetails_ProductId");
    }
}