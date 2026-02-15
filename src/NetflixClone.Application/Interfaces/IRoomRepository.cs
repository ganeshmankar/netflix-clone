using NetflixClone.Domain.Entities;

namespace NetflixClone.Application.Interfaces;

/// <summary>
/// Repository interface for Room (Watch Party) operations
/// </summary>
public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Room?> GetByRoomCodeAsync(string roomCode, CancellationToken cancellationToken = default);
    Task<List<Room>> GetActiveRoomsAsync(CancellationToken cancellationToken = default);
    Task<Room> AddAsync(Room room, CancellationToken cancellationToken = default);
    Task UpdateAsync(Room room, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
