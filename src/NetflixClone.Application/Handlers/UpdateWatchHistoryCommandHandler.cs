using MediatR;
using NetflixClone.Application.Commands;
using NetflixClone.Application.Interfaces;
using NetflixClone.Domain.Entities;
using NetflixClone.Domain.Events;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for UpdateWatchHistoryCommand
/// Updates viewing progress and raises UserWatchedMovieEvent
/// </summary>
public class UpdateWatchHistoryCommandHandler : IRequestHandler<UpdateWatchHistoryCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public UpdateWatchHistoryCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(UpdateWatchHistoryCommand request, CancellationToken cancellationToken)
    {
        // Calculate percentage watched
        var percentageWatched = (decimal)request.LastWatchedPositionSeconds / request.MovieDurationSeconds * 100;
        var isCompleted = percentageWatched >= 95; // Consider completed if >= 95%

        // Check if watch history exists
        var existing = await _unitOfWork.WatchHistories.GetByUserAndMovieAsync(
            request.UserId,
            request.MovieId,
            cancellationToken
        );

        if (existing != null)
        {
            // Update existing
            existing.LastWatchedPositionSeconds = request.LastWatchedPositionSeconds;
            existing.PercentageWatched = percentageWatched;
            existing.IsCompleted = isCompleted;
            existing.LastWatchedAt = DateTime.UtcNow;
            existing.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.WatchHistories.UpdateAsync(existing, cancellationToken);
        }
        else
        {
            // Create new
            var watchHistory = new WatchHistory
            {
                UserId = request.UserId,
                MovieId = request.MovieId,
                LastWatchedPositionSeconds = request.LastWatchedPositionSeconds,
                PercentageWatched = percentageWatched,
                IsCompleted = isCompleted,
                LastWatchedAt = DateTime.UtcNow
            };

            await _unitOfWork.WatchHistories.AddAsync(watchHistory, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Raise domain event for analytics and recommendations
        var watchedEvent = new UserWatchedMovieEvent(
            request.UserId,
            request.MovieId,
            request.LastWatchedPositionSeconds,
            percentageWatched,
            isCompleted
        );
        await _publisher.Publish(watchedEvent, cancellationToken);

        return Unit.Value;
    }
}
