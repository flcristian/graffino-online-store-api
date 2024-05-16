using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Repository.Interfaces;
using graffino_online_store_api.Products.Services.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Exceptions;

namespace graffino_online_store_api.Products.Services;

public class ProductsCommandService(IProductsRepository repository) : IProductsCommandService
{
    public async Task<GetClothingResponse> CreateClothing(CreateClothingRequest request)
    {
        if (request.Price <= 0)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_PRODUCT_PRICE);
        }

        GetClothingResponse clothing = await repository.CreateClothingAsync(request);
        return clothing;
    }

    public async Task<GetTVResponse> CreateTV(CreateTVRequest request)
    {
        if (request.Price <= 0)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_PRODUCT_PRICE);
        }

        GetTVResponse tv = await repository.CreateTVAsync(request);
        return tv;
    }

    public async Task<GetClothingResponse> UpdateClothing(UpdateClothingRequest request)
    {
        if (request.Price <= 0)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_PRODUCT_PRICE);
        }
        
        if (await repository.GetClothingByIdAsync(request.Id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }
        
        GetClothingResponse clothing = await repository.UpdateClothingAsync(request);
        return clothing;
    }

    public async Task<GetTVResponse> UpdateTV(UpdateTVRequest request)
    {
        if (request.Price <= 0)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_PRODUCT_PRICE);
        }

        if (await repository.GetTVByIdAsync(request.Id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }
        
        GetTVResponse tv = await repository.UpdateTVAsync(request);
        return tv;
    }

    public async Task<GetClothingResponse> DeleteClothingById(int id)
    {
        if (await repository.GetClothingByIdAsync(id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }
        
        GetClothingResponse clothing = await repository.DeleteClothingByIdAsync(id);
        return clothing;
    }

    public async Task<GetTVResponse> DeleteTVById(int id)
    {
        if (await repository.GetTVByIdAsync(id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }

        GetTVResponse tv = await repository.DeleteTVByIdAsync(id);
        return tv;
    }
}