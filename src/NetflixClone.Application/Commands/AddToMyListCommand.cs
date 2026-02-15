using MediatR;

namespace NetflixClone.Application.Commands;

/// <summary>
/// Command to add a movie to user's My List
/// </summary>
public class AddToMyListCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid MovieId { get; set; }

    public AddToMyListCommand(Guid userId, Guid movieId)
    {
        UserId = userId;
        MovieId = movieId;
    }
}
