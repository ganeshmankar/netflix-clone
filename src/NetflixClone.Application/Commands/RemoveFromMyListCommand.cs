using MediatR;

namespace NetflixClone.Application.Commands;

/// <summary>
/// Command to remove a movie from user's My List
/// </summary>
public class RemoveFromMyListCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }
    public Guid MovieId { get; set; }

    public RemoveFromMyListCommand(Guid userId, Guid movieId)
    {
        UserId = userId;
        MovieId = movieId;
    }
}
