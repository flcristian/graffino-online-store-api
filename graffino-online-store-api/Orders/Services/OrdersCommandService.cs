using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.Orders.Repository.Interfaces;
using graffino_online_store_api.Orders.Services.Interfaces;

namespace graffino_online_store_api.Orders.Services;

public class OrdersCommandService(IOrdersRepository repository) : IOrdersCommandService
{
    public async Task<Order> CreateOrder(CreateOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> UpdateOrder(UpdateOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> DeleteOrderById(int id)
    {
        throw new NotImplementedException();
    }
}