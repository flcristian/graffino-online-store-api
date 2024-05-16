using graffino_online_store_api.OrderDetails.DTOs;
using graffino_online_store_api.OrderDetails.Models;
using graffino_online_store_api.OrderDetails.Repository.Interfaces;
using graffino_online_store_api.OrderDetails.Services.Interfaces;

namespace graffino_online_store_api.OrderDetails.Services;

public class OrderDetailsCommandService(IOrderDetailsRepository repository) : IOrderDetailsCommandService
{
    public async Task<OrderDetail> CreateOrderDetail(CreateOrderDetailRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderDetail> UpdateOrderDetail(UpdateOrderDetailRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderDetail> DeleteOrderDetailById(int id)
    {
        throw new NotImplementedException();
    }
}