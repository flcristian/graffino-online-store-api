using graffino_online_store_api.Orders.Models;

namespace graffino_online_store_api.Orders.DTOs;

public class UpdateOrderRequest
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
}