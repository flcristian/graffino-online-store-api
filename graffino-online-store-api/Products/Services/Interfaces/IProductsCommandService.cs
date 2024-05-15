using graffino_online_store_api.Products.DTOs;

namespace graffino_online_store_api.Products.Services.Interfaces;

public interface IProductsCommandService
{
    Task<GetClothingResponse> CreateClothing(CreateProductRequest request);
    Task<GetTVResponse> CreateTV(CreateTVRequest request);
    Task<GetClothingResponse> UpdateClothing(UpdateClothingRequest request);
    Task<GetTVResponse> UpdateTV(UpdateTVRequest request);
    Task<GetClothingResponse> DeleteClothingById(int id);
    Task<GetTVResponse> DeleteTVById(int id);
}