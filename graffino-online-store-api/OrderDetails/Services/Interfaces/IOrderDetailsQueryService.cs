using graffino_online_store_api.OrderDetails.Models;

namespace graffino_online_store_api.OrderDetails.Services.Interfaces;

public interface IOrderDetailsQueryService
{
    Task<IEnumerable<OrderDetail>> GetAllOrderDetails();
    Task<OrderDetail> GetOrderDetailById(int id);
}