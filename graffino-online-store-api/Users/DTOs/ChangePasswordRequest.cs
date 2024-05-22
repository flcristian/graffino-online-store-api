namespace graffino_online_store_api.Users.DTOs;

public class ChangePasswordRequest
{
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}