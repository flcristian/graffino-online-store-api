using graffino_online_store_api.Products.Models;

namespace graffino_online_store_api.Products.DTOs;

public abstract class CreateProductRequest
{
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
}