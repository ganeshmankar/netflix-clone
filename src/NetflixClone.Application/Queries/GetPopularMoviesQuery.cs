using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Queries;

/// <summary>
/// Query to get popular movies (sorted by popularity score)
/// Returns top N movies
/// </summary>
public class GetPopularMoviesQuery : IRequest<List<MovieDto>>
{
    public int Count { get; set; }

    public GetPopularMoviesQuery(int count = 20)
    {
        Count = count;
    }
}
