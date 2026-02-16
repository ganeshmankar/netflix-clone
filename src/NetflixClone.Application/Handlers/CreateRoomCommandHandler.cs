using MediatR;
using NetflixClone.Application.Commands;
using NetflixClone.Application.Interfaces;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for creating watch party rooms
/// </summary>
public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        // Generate unique room code
        var roomCode = GenerateRoomCode();

        // Create room entity
        var room = new Room
        {
            Name = request.Name,
            HostUserId = request.HostUserId,
            MovieId = request.MovieId,
            RoomCode = roomCode,
            IsActive = true,
            IsPlaying = false,
            CurrentPositionSeconds = 0
        };

        // Save to database
        await _unitOfWork.Rooms.AddAsync(room, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return roomCode;
    }

    private string GenerateRoomCode()
    {
        // Generate 6-character alphanumeric code
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
