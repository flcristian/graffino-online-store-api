namespace graffino_online_store_api.Users.DTOs;

public class GetUserDetailsResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public IEnumerable<string>  Roles { get; set; }
}