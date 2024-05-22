using graffino_online_store_api.OrderDetails.DTOs;

namespace graffino_online_store_api.Orders.DTOs;

public class CreateOrderRequest
{
    public string CustomerId { get; set; }
    public List<CreateOrderDetailRequest> OrderDetails { get; set; }
}