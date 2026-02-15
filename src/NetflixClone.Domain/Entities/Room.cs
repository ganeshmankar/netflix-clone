using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities;

/// <summary>
/// Watch Party room for synchronized viewing
/// Implements real-time watch party feature using SignalR
/// </summary>
public class Room : BaseEntity
{
    /// <summary>
    /// Unique room code for joining (e.g., "ABC123")
    /// </summary>
    public string RoomCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Room name/title
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// User who created the room (host)
    /// </summary>
    public Guid HostUserId { get; set; }
    
    /// <summary>
    /// Movie being watched in this room
    /// </summary>
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    
    /// <summary>
    /// Current playback position in seconds (synchronized across all participants)
    /// </summary>
    public int CurrentPositionSeconds { get; set; }
    
    /// <summary>
    /// Whether the video is currently playing or paused
    /// </summary>
    public bool IsPlaying { get; set; }
    
    /// <summary>
    /// Whether the room is active or closed
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// When the room was closed
    /// </summary>
    public DateTime? ClosedAt { get; set; }
    
    // Navigation properties
    public ICollection<RoomParticipant> Participants { get; set; } = new List<RoomParticipant>();
}
