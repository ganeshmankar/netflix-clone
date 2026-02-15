using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Queries;

/// <summary>
/// Query to search movies by title, description, cast, or director
/// Returns matching movies
/// </summary>
public class SearchMoviesQuery : IRequest<List<MovieDto>>
{
    public string SearchTerm { get; set; }

    public SearchMoviesQuery(string searchTerm)
    {
        SearchTerm = searchTerm;
    }
}
