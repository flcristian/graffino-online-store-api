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
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Property> Properties { get; set; }
    public virtual DbSet<ProductProperty> ProductProperties { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<ProductProperty>()
            .HasOne(pp => pp.Product)
            .WithMany(p => p.ProductProperties)
            .HasForeignKey(pp => pp.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<ProductProperty>()
            .HasOne(pp => pp.Property)
            .WithMany(p => p.ProductProperties)
            .HasForeignKey(pp => pp.PropertyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<Property>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Properties)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

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
    }
}