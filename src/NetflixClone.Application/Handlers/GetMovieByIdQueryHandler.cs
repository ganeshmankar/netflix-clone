using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces;
using NetflixClone.Application.Queries;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for GetMovieByIdQuery
/// Retrieves a single movie with full details
/// </summary>
public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieDetailDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMovieByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MovieDetailDto?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(request.MovieId, cancellationToken);

        if (movie == null)
            return null;

        // Map to detailed DTO
        var dto = new MovieDetailDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseYear = movie.ReleaseYear,
            DurationMinutes = movie.DurationMinutes,
            VideoUrl = movie.VideoUrl,
            ThumbnailUrl = movie.ThumbnailUrl,
            BannerUrl = movie.BannerUrl,
            Rating = movie.Rating,
            AverageRating = movie.AverageRating,
            PopularityScore = movie.PopularityScore,
            ViewCount = movie.ViewCount,
            Director = movie.Director,
            Cast = movie.Cast,
            Genres = movie.MovieGenres.Select(mg => mg.Genre).ToList(),
            CreatedAt = movie.CreatedAt
        };

        return dto;
    }
}
