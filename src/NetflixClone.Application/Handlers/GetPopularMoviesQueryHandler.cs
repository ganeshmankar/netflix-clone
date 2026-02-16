using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces;
using NetflixClone.Application.Queries;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for getting popular movies
/// </summary>
public class GetPopularMoviesQueryHandler : IRequestHandler<GetPopularMoviesQuery, List<MovieDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPopularMoviesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MovieDto>> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _unitOfWork.Movies.GetPopularAsync(request.Count, cancellationToken);

        // Map to DTOs
        var movieDtos = movies.Select(m => new MovieDto
        {
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            ReleaseYear = m.ReleaseYear,
            DurationMinutes = m.DurationMinutes,
            ThumbnailUrl = m.ThumbnailUrl,
            Rating = m.Rating,
            AverageRating = m.AverageRating,
            Genres = m.MovieGenres.Select(mg => mg.Genre).ToList()
        }).ToList();

        return movieDtos;
    }
}
