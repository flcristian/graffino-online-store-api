namespace graffino_online_store_api.Orders.Models;

public enum OrderStatus
{
    None = 0,
    Processing = 1,
    Shipping = 2,
    Shipped = 3,
    Complete = 4
}