namespace graffino_online_store_api.OrderDetails.DTOs;

public class CreateOrderDetailRequest
{
    public int ProductId { get; set; }
    public int Count { get; set; }
}