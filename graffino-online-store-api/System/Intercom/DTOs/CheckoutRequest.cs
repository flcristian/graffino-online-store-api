using graffino_online_store_api.Orders.DTOs;

namespace graffino_online_store_api.System.Intercom.DTOs;

public class CheckoutRequest
{
    public List<CheckoutProductDetailDTO> ProductDetails { get; set; }
    public CreateOrderRequest OrderRequest { get; set; }
}