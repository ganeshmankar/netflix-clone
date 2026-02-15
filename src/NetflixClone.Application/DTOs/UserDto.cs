namespace NetflixClone.Application.DTOs;

/// <summary>
/// User information for client
/// </summary>
public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public string SubscriptionTier { get; set; } = "Free";
}
