using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;

namespace graffino_online_store_api.Orders.Repository.Interfaces;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<Order?> GetCartByCustomerIdAsync(string customerId); 
    Task<Order> CreateAsync(CreateOrderRequest request);
    Task<Order> UpdateAsync(UpdateOrderRequest request);
    Task<Order> DeleteByIdAsync(int id);
}