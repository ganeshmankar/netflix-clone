using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces;
using NetflixClone.Application.Queries;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for getting continue watching list
/// </summary>
public class GetContinueWatchingQueryHandler : IRequestHandler<GetContinueWatchingQuery, List<WatchHistoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetContinueWatchingQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<WatchHistoryDto>> Handle(GetContinueWatchingQuery request, CancellationToken cancellationToken)
    {
        var watchHistories = await _unitOfWork.WatchHistories.GetContinueWatchingAsync(
            request.UserId, 
            request.Count, 
            cancellationToken);

        // Map to DTOs
        var dtos = watchHistories.Select(wh => new WatchHistoryDto
        {
            Id = wh.Id,
            UserId = wh.UserId,
            MovieId = wh.MovieId,
            LastWatchedPositionSeconds = wh.LastWatchedPositionSeconds,
            PercentageWatched = wh.PercentageWatched,
            IsCompleted = wh.IsCompleted,
            LastWatchedAt = wh.LastWatchedAt,
            Movie = new MovieDto
            {
                Id = wh.Movie.Id,
                Title = wh.Movie.Title,
                Description = wh.Movie.Description,
                ReleaseYear = wh.Movie.ReleaseYear,
                DurationMinutes = wh.Movie.DurationMinutes,
                ThumbnailUrl = wh.Movie.ThumbnailUrl,
                Rating = wh.Movie.Rating,
                AverageRating = wh.Movie.AverageRating,
                Genres = wh.Movie.MovieGenres.Select(mg => mg.Genre).ToList()
            }
        }).ToList();

        return dtos;
    }
}
