using graffino_online_store_api.Products.DTOs;

namespace graffino_online_store_api.Products.Services.Interfaces;

public interface IProductsQueryService
{
    Task<Dictionary<string, IEnumerable<object>>> GetAllProducts();
    Task<IEnumerable<GetClothingResponse>> GetAllClothing();
    Task<IEnumerable<GetTVResponse>> GetAllTelevisions();
    Task<GetClothingResponse> GetClothingById(int id);
    Task<GetTVResponse> GetTVById(int id);
}