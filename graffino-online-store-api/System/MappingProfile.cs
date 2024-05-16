using AutoMapper;
using graffino_online_store_api.OrderDetails.DTOs;
using graffino_online_store_api.OrderDetails.Models;
using graffino_online_store_api.Products.DTOs;
using graffino_online_store_api.Products.Models;

namespace graffino_online_store_api.Products.Repository;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateClothingRequest, Clothing>();
        CreateMap<CreateTVRequest, TV>();
        CreateMap<CreateOrderDetailRequest, OrderDetail>();

    }
}