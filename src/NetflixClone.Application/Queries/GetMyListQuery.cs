using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Queries;

/// <summary>
/// Query to get user's My List (favorites)
/// Returns all movies in user's list
/// </summary>
public class GetMyListQuery : IRequest<List<MovieDto>>
{
    public Guid UserId { get; set; }

    public GetMyListQuery(Guid userId)
    {
        UserId = userId;
    }
}
