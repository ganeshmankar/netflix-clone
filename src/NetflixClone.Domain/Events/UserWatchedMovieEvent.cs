namespace NetflixClone.Domain.Events;

/// <summary>
/// Domain event raised when a user watches a movie
/// Triggers: watch history update, popularity score update, recommendations
/// </summary>
public class UserWatchedMovieEvent : BaseDomainEvent
{
    public Guid UserId { get; }
    public Guid MovieId { get; }
    public int WatchedPositionSeconds { get; }
    public decimal PercentageWatched { get; }
    public bool IsCompleted { get; }

    public UserWatchedMovieEvent(
        Guid userId, 
        Guid movieId, 
        int watchedPositionSeconds, 
        decimal percentageWatched,
        bool isCompleted)
    {
        UserId = userId;
        MovieId = movieId;
        WatchedPositionSeconds = watchedPositionSeconds;
        PercentageWatched = percentageWatched;
        IsCompleted = isCompleted;
    }
}
