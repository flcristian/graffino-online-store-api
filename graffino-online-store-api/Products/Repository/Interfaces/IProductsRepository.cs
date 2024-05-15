using graffino_online_store_api.Products.DTOs;

namespace graffino_online_store_api.Products.Repository.Interfaces;

public interface IProductsRepository
{
    Task<IEnumerable<GetClothingResponse>> GetAllClothingAsync();
    Task<IEnumerable<GetTVResponse>> GetAllTelevisionsAsync();
    Task<GetClothingResponse?> GetClothingByIdAsync(int id);
    Task<GetTVResponse?> GetTVByIdAsync(int id);
    Task<GetClothingResponse> CreateClothingAsync(CreateClothingRequest request);
    Task<GetTVResponse> CreateTVAsync(CreateTVRequest request);
    Task<GetClothingResponse> UpdateClothingAsync(UpdateClothingRequest request);
    Task<GetTVResponse> UpdateTVAsync(UpdateTVRequest request);
    Task<GetClothingResponse> DeleteClothingByIdAsync(int id);
    Task<GetTVResponse> DeleteTVByIdAsync(int id);
}