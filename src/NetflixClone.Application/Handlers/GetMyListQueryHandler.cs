using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces;
using NetflixClone.Application.Queries;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for getting user's My List
/// </summary>
public class GetMyListQueryHandler : IRequestHandler<GetMyListQuery, List<MovieDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMyListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MovieDto>> Handle(GetMyListQuery request, CancellationToken cancellationToken)
    {
        var myListItems = await _unitOfWork.MyLists.GetByUserIdAsync(request.UserId, cancellationToken);

        // Map to DTOs
        var movieDtos = myListItems.Select(ml => new MovieDto
        {
            Id = ml.Movie.Id,
            Title = ml.Movie.Title,
            Description = ml.Movie.Description,
            ReleaseYear = ml.Movie.ReleaseYear,
            DurationMinutes = ml.Movie.DurationMinutes,
            ThumbnailUrl = ml.Movie.ThumbnailUrl,
            Rating = ml.Movie.Rating,
            AverageRating = ml.Movie.AverageRating,
            Genres = ml.Movie.MovieGenres.Select(mg => mg.Genre).ToList()
        }).ToList();

        return movieDtos;
    }
}
