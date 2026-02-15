namespace NetflixClone.Domain.Events;

/// <summary>
/// Domain event raised when a new movie is uploaded to the system
/// Triggers: cache invalidation, notification to users, indexing
/// </summary>
public class MovieUploadedEvent : BaseDomainEvent
{
    public Guid MovieId { get; }
    public string Title { get; }
    public Guid UploadedByUserId { get; }

    public MovieUploadedEvent(Guid movieId, string title, Guid uploadedByUserId)
    {
        MovieId = movieId;
        Title = title;
        UploadedByUserId = uploadedByUserId;
    }
}
