namespace graffino_online_store_api.Products.DTOs;

public class UpdateTVRequest : UpdateProductRequest
{
    public string Diameter { get; set; }
    public string Resolution { get; set; }
}