namespace NetflixClone.Domain.Events;

/// <summary>
/// Domain event raised when a user adds a movie to their list
/// Triggers: recommendation engine update, user preference tracking
/// </summary>
public class MovieAddedToListEvent : BaseDomainEvent
{
    public Guid UserId { get; }
    public Guid MovieId { get; }

    public MovieAddedToListEvent(Guid userId, Guid movieId)
    {
        UserId = userId;
        MovieId = movieId;
    }
}
