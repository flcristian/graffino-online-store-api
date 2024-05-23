using AutoMapper;
using graffino_online_store_api.Data;
using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Models;
using graffino_online_store_api.Products.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace graffino_online_store_api.Products.Repository;

public class ProductsRepository(
    AppDbContext context,
    IMapper mapper
    ) : IProductsRepository
{
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await context.Categories
            .Include(c => c.Properties)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductProperties)
            .ThenInclude(pp => pp.Property)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductProperties)
            .ThenInclude(pp => pp.Property)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        return await context.Categories
            .Include(c => c.Properties)
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }
    
    public async Task<Category?> GetCategoryByNameAsync(string categoryName)
    {
        return await context.Categories
            .Include(c => c.Properties)
            .FirstOrDefaultAsync(c => c.Name.Equals(categoryName));
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductProperties)
            .ThenInclude(pp => pp.Property)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Category> CreateCategoryAsync(CreateCategoryRequest request)
    {
        Category category = new()
        {
            Name = request.Name
        };
        context.Categories.Add(category);

        category.Properties = new List<Property>();
        request.Properties.ForEach(p =>
        {
            Property property = mapper.Map<Property>(p);
            category.Properties.Add(property);
        });

        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Product> CreateProductAsync(CreateProductRequest request)
    {
        Product product = new()
        {
            Name = request.Name,
            Price = request.Price,
            DateAdded = DateTime.Now,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId
        };
        context.Products.Add(product);

        product.ProductProperties = new List<ProductProperty>();
        request.ProductProperties.ForEach(pp =>
        {
            ProductProperty productProperty = mapper.Map<ProductProperty>(pp);
            product.ProductProperties.Add(productProperty);
        });

        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Category> UpdateCategoryAsync(UpdateCategoryRequest request)
    {
        Category category = (await GetCategoryByIdAsync(request.Id))!;
        category.Name = request.Name;
        context.Categories.Update(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Product> UpdateProductAsync(UpdateProductRequest request)
    {
        Product product = (await GetProductByIdAsync(request.Id))!;
        product.Name = request.Name;
        product.Price = request.Price;
        product.ImageUrl = request.ImageUrl;
        context.Products.Update(product);

        product.ProductProperties = new List<ProductProperty>();
        request.ProductProperties.ForEach(pp =>
        {
            ProductProperty productProperty = mapper.Map<ProductProperty>(pp);
            product.ProductProperties.Add(productProperty);
        });

        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Category> DeleteCategoryByIdAsync(int categoryId)
    {
        Category category = (await context.Categories.FindAsync(categoryId))!;
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Product> DeleteProductByIdAsync(int productId)
    {
        Product product = (await context.Products.FindAsync(productId))!;
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> FilterProducts(int? categoryId, string? search, Dictionary<string, string> properties, int? page, int? itemsPerPage)
    {
        properties.Remove("categoryId");
        properties.Remove("search");
        properties.Remove("page");
        properties.Remove("itemsPerPage");
        
        IEnumerable<Product> products = await context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductProperties)
            .ThenInclude(pp => pp.Property)
            .ToListAsync();

        if (categoryId.HasValue)
        {
            products = products.Where(p => p.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            products = products.Where(p => p.Name.ToLower().Contains(search.ToLower()) ||
                p.ProductProperties.Any(pp => pp.Value.ToLower().Contains(search.ToLower())));
        }

        foreach (var property in properties)
        {
            products = products.Where(p => p.ProductProperties.Any(pp => 
                pp.Property.Name.ToLower().Equals(property.Key.ToLower()) && pp.Value.ToLower().Equals(property.Value.ToLower())));
        }
        
        if (page.HasValue && itemsPerPage.HasValue)
        {
            int pageNumber = page.Value;
            int pageSize = itemsPerPage.Value;

            products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        return products;
    }
}