using AutoMapper;
using graffino_online_store_api.Data;
using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Models;
using graffino_online_store_api.Products.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace graffino_online_store_api.Products.Repository;

public class ProductsRepository(IMapper mapper, AppDbContext context) : IProductsRepository
{
    public async Task<IEnumerable<GetClothingResponse>> GetAllClothingAsync()
    {
        return await context.Clothing
            .Join(
                context.Products,
                clothing => clothing.Id,
                product => product.Id,
                (clothing, product) => new GetClothingResponse()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    DateAdded = product.DateAdded,
                    Category = Category.Clothing,
                    Color = clothing.Color,
                    Style = clothing.Style,
                    Size = clothing.Size,
                })
            .ToListAsync();
    }

    public async Task<IEnumerable<GetTVResponse>> GetAllTelevisionsAsync()
    {
        return await context.Televisions
            .Join(
                context.Products,
                tv => tv.Id,
                product => product.Id,
                (tv, product) => new GetTVResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    DateAdded = product.DateAdded,
                    Category = Category.Television,
                    Diameter = tv.Diameter,
                    Resolution = tv.Resolution
                })
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<GetClothingResponse?> GetClothingByIdAsync(int id)
    {
        Product? product = await context.Products.FindAsync(id);
        
        if (product != null)
        {
            Clothing clothing = (await context.Clothing.FindAsync(id))!;
            return ConvertProductAndClothingToGetResponse(product, clothing);
        }

        return null;
    }
    
    public async Task<GetTVResponse?> GetTVByIdAsync(int id)
    {
        Product? product = await context.Products.FindAsync(id);
        
        if (product != null)
        {
            TV tv = (await context.Televisions.FindAsync(id))!;
            return ConvertProductAndTVToGetResponse(product, tv);
        }

        return null;
    }
    
    public async Task<GetClothingResponse> CreateClothingAsync(CreateClothingRequest request)
    {
        Clothing clothing = mapper.Map<Clothing>(request);
        clothing.DateAdded = DateTime.Now;
        context.Clothing.Add(clothing);
        await context.SaveChangesAsync();
        return ConvertClothingToGetResponse(clothing);
    }

    public async Task<GetTVResponse> CreateTVAsync(CreateTVRequest request)
    {
        TV tv = mapper.Map<TV>(request);
        tv.DateAdded = DateTime.Now;
        context.Televisions.Add(tv);
        await context.SaveChangesAsync();
        return ConvertTVToGetResponse(tv);
    }

    public async Task<GetClothingResponse> UpdateClothingAsync(UpdateClothingRequest request)
    {
        Product product = (await context.Products.FindAsync(request.Id))!;
        Clothing clothing = (await context.Clothing.FindAsync(request.Id))!;

        product.Name = request.Name;
        product.Price = request.Price;

        clothing.Color = request.Color;
        clothing.Style = request.Style;
        clothing.Size = request.Size;
        
        context.Products.Update(product);
        context.Clothing.Update(clothing);
        await context.SaveChangesAsync();

        return ConvertProductAndClothingToGetResponse(product, clothing);
    }

    public async Task<GetTVResponse> UpdateTVAsync(UpdateTVRequest request)
    {
        Product product = (await context.Products.FindAsync(request.Id))!;
        TV tv = (await context.Televisions.FindAsync(request.Id))!;

        product.Name = request.Name;
        product.Price = request.Price;

        tv.Diameter = request.Diameter;
        tv.Resolution = request.Resolution;
        
        context.Products.Update(product);
        context.Televisions.Update(tv);
        await context.SaveChangesAsync();

        return ConvertProductAndTVToGetResponse(product, tv);
    }

    public async Task<GetClothingResponse> DeleteClothingByIdAsync(int id)
    {
        Product product = (await context.Products.FindAsync(id))!;
        Clothing clothing = (await context.Clothing.FindAsync(id))!;
        context.Clothing.Remove(clothing);
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return ConvertProductAndClothingToGetResponse(product, clothing);
    }
    
    public async Task<GetTVResponse> DeleteTVByIdAsync(int id)
    {
        Product product = (await context.Products.FindAsync(id))!;
        TV tv = (await context.Televisions.FindAsync(id))!;
        context.Televisions.Remove(tv);
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return ConvertProductAndTVToGetResponse(product, tv);
    }
    
    #region PRIVATE METHODS

    private GetClothingResponse ConvertClothingToGetResponse(Clothing clothing)
    {
        return new GetClothingResponse
        {
            Id = clothing.Id,
            Name = clothing.Name,
            Price = clothing.Price,
            DateAdded = clothing.DateAdded,
            Category = Category.Television,
            Color = clothing.Color,
            Style = clothing.Style,
            Size = clothing.Size
        };
    }

    private GetTVResponse ConvertTVToGetResponse(TV tv)
    {
        return new GetTVResponse
        {
            Id = tv.Id,
            Name = tv.Name,
            Price = tv.Price,
            DateAdded = tv.DateAdded,
            Category = Category.Television,
            Diameter = tv.Diameter,
            Resolution = tv.Resolution
        };
    }

    private GetClothingResponse ConvertProductAndClothingToGetResponse(Product product, Clothing clothing)
    {
        return new GetClothingResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            DateAdded = product.DateAdded,
            Category = Category.Clothing,
            Color = clothing.Color,
            Style = clothing.Style,
            Size = clothing.Size,
        };
    }
    
    private GetTVResponse ConvertProductAndTVToGetResponse(Product product, TV tv)
    {
        return new GetTVResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            DateAdded = product.DateAdded,
            Category = Category.Television,
            Diameter = tv.Diameter,
            Resolution = tv.Resolution
        };
    }
    
    #endregion
}