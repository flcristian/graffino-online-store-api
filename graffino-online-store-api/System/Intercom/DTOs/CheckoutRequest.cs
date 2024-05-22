using graffino_online_store_api.OrderDetails.DTOs;

namespace graffino_online_store_api.Products.Repository.Intercom.DTOs;

public class CheckoutRequest
{
    private List<CreateOrderDetailRequest> OrderDetails { get; set; }
}