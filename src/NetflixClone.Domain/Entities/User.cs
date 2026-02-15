using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities;

/// <summary>
/// Application user entity
/// Note: This works alongside ASP.NET Identity's IdentityUser
/// </summary>
public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Profile picture URL
    /// </summary>
    public string? ProfilePictureUrl { get; set; }
    
    /// <summary>
    /// User's subscription tier (for future expansion)
    /// </summary>
    public string SubscriptionTier { get; set; } = "Free";
    
    /// <summary>
    /// Last login timestamp
    /// </summary>
    public DateTime? LastLoginAt { get; set; }
    
    // Navigation properties
    public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
    public ICollection<MyListItem> MyList { get; set; } = new List<MyListItem>();
    public ICollection<RoomParticipant> RoomParticipations { get; set; } = new List<RoomParticipant>();
}
