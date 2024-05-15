using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Repository.Interfaces;
using graffino_online_store_api.Products.Services.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Exceptions;

namespace graffino_online_store_api.Products.Services;

public class ProductsQueryService(IProductsRepository repository) : IProductsQueryService
{
    public async Task<Dictionary<string, IEnumerable<object>>> GetAllProducts()
    {
        List<GetClothingResponse> clothing = (await repository.GetAllClothingAsync()).ToList();
        List<GetTVResponse> televisions = (await repository.GetAllTelevisionsAsync()).ToList();

        if (clothing.Count == 0 && televisions.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.PRODUCTS_DO_NOT_EXIST);
        }
        
        Dictionary<string, IEnumerable<object>> result = new Dictionary<string, IEnumerable<object>>
        {
            { "Clothing", clothing },
            { "Television", televisions }
        };

        return result;
    }

    public async Task<IEnumerable<GetClothingResponse>> GetAllClothing()
    {
        List<GetClothingResponse> clothing = (await repository.GetAllClothingAsync()).ToList();

        if (clothing.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.PRODUCTS_DO_NOT_EXIST);
        }

        return clothing;
    }

    public async Task<IEnumerable<GetTVResponse>> GetAllTelevisions()
    {
        List<GetTVResponse> televisions = (await repository.GetAllTelevisionsAsync()).ToList();

        if (televisions.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.PRODUCTS_DO_NOT_EXIST);
        }

        return televisions;
    }

    public async Task<GetClothingResponse> GetClothingById(int id)
    {
        GetClothingResponse? clothing = await repository.GetClothingByIdAsync(id);

        if (clothing == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }

        return clothing;
    }

    public async Task<GetTVResponse> GetTVById(int id)
    {
        GetTVResponse? tv = await repository.GetTVByIdAsync(id);

        if (tv == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }

        return tv;
    }
}