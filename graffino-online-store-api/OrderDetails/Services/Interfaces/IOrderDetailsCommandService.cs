using graffino_online_store_api.OrderDetails.DTOs;
using graffino_online_store_api.OrderDetails.Models;

namespace graffino_online_store_api.OrderDetails.Services.Interfaces;

public interface IOrderDetailsCommandService
{
    Task<OrderDetail> CreateOrderDetail(CreateOrderDetailRequest request);
    Task<OrderDetail> UpdateOrderDetail(UpdateOrderDetailRequest request);
    Task<OrderDetail> DeleteOrderDetailById(int id);
}