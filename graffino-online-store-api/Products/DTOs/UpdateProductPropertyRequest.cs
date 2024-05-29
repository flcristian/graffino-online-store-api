namespace graffino_online_store_api.Products.DTOs;

public class UpdateProductPropertyRequest
{
    public int PropertyId { get; set; }
    public string Value { get; set; }
}