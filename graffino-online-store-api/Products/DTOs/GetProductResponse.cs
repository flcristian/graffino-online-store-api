using graffino_online_store_api.Products.Models;

namespace graffino_online_store_api.Products.DTOs;

public abstract class GetProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public DateTime DateAdded { get; set; }
    public string ImageUrl { get; set; }
}