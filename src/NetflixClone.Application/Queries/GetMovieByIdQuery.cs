using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Queries;

/// <summary>
/// Query to get a single movie by ID
/// Returns MovieDetailDto with full information
/// </summary>
public class GetMovieByIdQuery : IRequest<MovieDetailDto?>
{
    public Guid MovieId { get; set; }

    public GetMovieByIdQuery(Guid movieId)
    {
        MovieId = movieId;
    }
}
