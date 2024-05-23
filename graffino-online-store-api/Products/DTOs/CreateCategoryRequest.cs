namespace graffino_online_store_api.Products.DTOs;

public class CreateCategoryRequest
{
    public string Name { get; set; }
    public List<CreatePropertyRequest> Properties { get; set; }
}