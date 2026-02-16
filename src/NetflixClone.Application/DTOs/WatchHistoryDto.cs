namespace NetflixClone.Application.DTOs;

/// <summary>
/// Watch history information for continue watching feature
/// </summary>
public class WatchHistoryDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid MovieId { get; set; }
    public MovieDto Movie { get; set; } = null!;
    public int LastWatchedPositionSeconds { get; set; }
    public decimal PercentageWatched { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime LastWatchedAt { get; set; }
}
