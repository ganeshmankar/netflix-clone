using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces;
using NetflixClone.Application.Queries;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for GetMoviesQuery
/// Retrieves all movies and maps them to DTOs
/// </summary>
public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, List<MovieDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMoviesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _unitOfWork.Movies.GetAllAsync(cancellationToken);

        // Map entities to DTOs
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
