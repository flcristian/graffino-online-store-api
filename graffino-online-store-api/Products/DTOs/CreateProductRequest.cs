namespace graffino_online_store_api.Products.DTOs;

public class CreateProductRequest
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public string ImageUrl { get; set; }
    public List<CreateProductPropertyRequest> ProductProperties { get; set; }
}