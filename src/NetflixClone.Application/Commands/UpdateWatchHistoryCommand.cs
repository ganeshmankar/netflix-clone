using MediatR;

namespace NetflixClone.Application.Commands;

/// <summary>
/// Command to update watch history
/// Tracks user's viewing progress
/// </summary>
public class UpdateWatchHistoryCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }
    public Guid MovieId { get; set; }
    public int LastWatchedPositionSeconds { get; set; }
    public int MovieDurationSeconds { get; set; }

    public UpdateWatchHistoryCommand(Guid userId, Guid movieId, int lastWatchedPositionSeconds, int movieDurationSeconds)
    {
        UserId = userId;
        MovieId = movieId;
        LastWatchedPositionSeconds = lastWatchedPositionSeconds;
        MovieDurationSeconds = movieDurationSeconds;
    }
}
