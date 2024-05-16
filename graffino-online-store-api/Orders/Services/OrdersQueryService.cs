using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.Orders.Repository.Interfaces;
using graffino_online_store_api.Orders.Services.Interfaces;

namespace graffino_online_store_api.Orders.Services;

public class OrdersQueryService(IOrdersRepository repository) : IOrdersQueryService
{
    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public async Task<Order> GetOrderById(int id)
    {
        throw new NotImplementedException();
    }
}