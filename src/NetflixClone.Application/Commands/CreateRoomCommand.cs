using MediatR;

namespace NetflixClone.Application.Commands;

/// <summary>
/// Command to create a watch party room
/// </summary>
public class CreateRoomCommand : IRequest<string>
{
    public string Name { get; set; } = string.Empty;
    public Guid HostUserId { get; set; }
    public Guid MovieId { get; set; }

    public CreateRoomCommand(string name, Guid hostUserId, Guid movieId)
    {
        Name = name;
        HostUserId = hostUserId;
        MovieId = movieId;
    }
}
