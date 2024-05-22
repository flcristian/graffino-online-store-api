namespace graffino_online_store_api.Orders.Models;

public enum OrderStatus
{
    None = 0,
    Cart = 1,
    Processing = 2,
    Shipping = 3,
    Shipped = 4,
    Complete = 5
}