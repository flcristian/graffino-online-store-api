using graffino_online_store_api.OrderDetails.DTOs;
using graffino_online_store_api.OrderDetails.Models;

namespace graffino_online_store_api.OrderDetails.Repository.Interfaces;

public interface IOrderDetailsRepository
{
    Task<IEnumerable<OrderDetail>> GetAllAsync();
    Task<OrderDetail?> GetByIdAsync(int id);
    Task<OrderDetail> CreateAsync(CreateOrderDetailRequest request);
    Task<OrderDetail> UpdateAsync(UpdateOrderDetailRequest request);
    Task<OrderDetail> DeleteByIdAsync(int id);
}