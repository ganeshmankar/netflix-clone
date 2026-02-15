using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities;

/// <summary>
/// Tracks user's watch history for movies
/// Used for "Continue Watching" feature and recommendations
/// </summary>
public class WatchHistory : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    
    /// <summary>
    /// Last watched position in seconds
    /// </summary>
    public int LastWatchedPositionSeconds { get; set; }
    
    /// <summary>
    /// Percentage of movie watched (0-100)
    /// </summary>
    public decimal PercentageWatched { get; set; }
    
    /// <summary>
    /// Whether the user completed watching the movie
    /// </summary>
    public bool IsCompleted { get; set; }
    
    /// <summary>
    /// Last time the user watched this movie
    /// </summary>
    public DateTime LastWatchedAt { get; set; } = DateTime.UtcNow;
}
