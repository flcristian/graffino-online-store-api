namespace graffino_online_store_api.Products.DTOs;

public class CreateTVRequest : CreateProductRequest
{
    public string Diameter { get; set; }
    public string Resolution { get; set; }
}