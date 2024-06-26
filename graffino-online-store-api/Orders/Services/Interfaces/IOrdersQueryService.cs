using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;

namespace graffino_online_store_api.Orders.Services.Interfaces;

public interface IOrdersQueryService
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<IEnumerable<Order>> GetAllOrdersByCustomerId(string customerId);
    Task<Order> GetOrderById(int id);
}