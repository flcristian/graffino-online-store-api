using graffino_online_store_api.OrderDetails.Models;
using graffino_online_store_api.OrderDetails.Repository.Interfaces;
using graffino_online_store_api.OrderDetails.Services.Interfaces;

namespace graffino_online_store_api.OrderDetails.Services;

public class OrderDetailsQueryService(IOrderDetailsRepository repository) : IOrderDetailsQueryService
{
    public async Task<IEnumerable<OrderDetail>> GetAllOrderDetails()
    {
        throw new NotImplementedException();
    }

    public async Task<OrderDetail> GetOrderDetailById(int id)
    {
        throw new NotImplementedException();
    }
}