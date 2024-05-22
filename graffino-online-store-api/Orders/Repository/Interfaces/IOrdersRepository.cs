using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;

namespace graffino_online_store_api.Orders.Repository.Interfaces;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetAllByCustomerIdAsync(string customerId);
    Task<Order?> GetByIdAsync(int id);
    Task<Order> CreateAsync(CreateOrderRequest request);
    Task<Order> UpdateAsync(UpdateOrderRequest request);
    Task<Order> DeleteByIdAsync(int id);
}