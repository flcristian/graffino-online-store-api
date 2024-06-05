using graffino_online_store_api.Products.Models;

namespace graffino_online_store_api.Products.DTOs;

public class FilterProductsResponse
{
    public List<Product> Products { get; set; }
    public int TotalPages { get; set; }
}