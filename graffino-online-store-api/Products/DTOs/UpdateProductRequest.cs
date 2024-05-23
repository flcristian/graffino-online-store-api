using graffino_online_store_api.Products.Models;

namespace graffino_online_store_api.Products.DTOs;

public class UpdateProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
}