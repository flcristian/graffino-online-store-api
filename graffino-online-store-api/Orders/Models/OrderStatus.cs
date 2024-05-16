namespace graffino_online_store_api.Orders.Models;

public enum OrderStatus
{
    None,
    Cart,
    Processing,
    Shipping,
    Shipped,
    Complete
}