using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;

namespace graffino_online_store_api.Orders.Services.Interfaces;

public interface IOrdersCommandService
{
    Task<Order> CreateOrder(CreateOrderRequest request);
    Task<Order> UpdateOrder(UpdateOrderRequest request);
    Task<Order> DeleteOrderById(int id);
}