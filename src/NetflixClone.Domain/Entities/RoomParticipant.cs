using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities;

/// <summary>
/// Represents a user participating in a watch party room
/// </summary>
public class RoomParticipant : BaseEntity
{
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    /// <summary>
    /// When the user joined the room
    /// </summary>
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// When the user left the room (null if still in room)
    /// </summary>
    public DateTime? LeftAt { get; set; }
    
    /// <summary>
    /// Whether the user is currently connected to the room
    /// </summary>
    public bool IsConnected { get; set; } = true;
}
