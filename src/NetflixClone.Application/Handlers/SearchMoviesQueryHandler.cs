using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces;
using NetflixClone.Application.Queries;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for searching movies
/// </summary>
public class SearchMoviesQueryHandler : IRequestHandler<SearchMoviesQuery, List<MovieDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public SearchMoviesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MovieDto>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _unitOfWork.Movies.SearchAsync(request.SearchTerm, cancellationToken);

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
