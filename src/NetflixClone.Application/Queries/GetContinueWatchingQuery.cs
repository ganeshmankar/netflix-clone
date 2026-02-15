using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Queries;

/// <summary>
/// Query to get user's continue watching list
/// Returns movies with watch progress < 95%
/// </summary>
public class GetContinueWatchingQuery : IRequest<List<WatchHistoryDto>>
{
    public Guid UserId { get; set; }
    public int Count { get; set; }

    public GetContinueWatchingQuery(Guid userId, int count = 10)
    {
        UserId = userId;
        Count = count;
    }
}
