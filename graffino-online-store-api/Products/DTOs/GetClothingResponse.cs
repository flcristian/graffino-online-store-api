namespace graffino_online_store_api.Products.DTOs;

public class GetClothingResponse : GetProductResponse
{
    public string Color { get; set; }
    public string Style { get; set; }
    public string Size { get; set; }
}