using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Queries;

/// <summary>
/// Query to get all movies
/// Returns a list of MovieDto
/// </summary>
public class GetMoviesQuery : IRequest<List<MovieDto>>
{
    // No parameters needed - get all movies
}
