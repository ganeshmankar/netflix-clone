using Microsoft.EntityFrameworkCore;
using NetflixClone.Application.Interfaces;
using NetflixClone.Domain.Entities;
using NetflixClone.Infrastructure.Data;

namespace NetflixClone.Infrastructure.Repositories;

/// <summary>
/// Implementation of IRoomRepository
/// Handles watch party room data access
/// </summary>
public class RoomRepository : IRoomRepository
{
    private readonly NetflixCloneDbContext _context;

    public RoomRepository(NetflixCloneDbContext context)
    {
        _context = context;
    }

    public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .Include(r => r.Participants)
                .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Room?> GetByRoomCodeAsync(string roomCode, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .Include(r => r.Participants)
                .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(r => r.RoomCode == roomCode, cancellationToken);
    }

    public async Task<List<Room>> GetActiveRoomsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .Include(r => r.Participants)
            .Where(r => r.IsActive)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Room> AddAsync(Room room, CancellationToken cancellationToken = default)
    {
        await _context.Rooms.AddAsync(room, cancellationToken);
        return room;
    }

    public async Task UpdateAsync(Room room, CancellationToken cancellationToken = default)
    {
        _context.Rooms.Update(room);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var room = await GetByIdAsync(id, cancellationToken);
        if (room != null)
        {
            room.IsDeleted = true; // Soft delete
            _context.Rooms.Update(room);
        }
    }
}
