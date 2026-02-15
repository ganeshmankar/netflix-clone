using Microsoft.EntityFrameworkCore;
using NetflixClone.Application.Interfaces;
using NetflixClone.Domain.Entities;
using NetflixClone.Infrastructure.Data;

namespace NetflixClone.Infrastructure.Repositories;

/// <summary>
/// Implementation of IMyListRepository
/// Handles user's favorite movies list
/// </summary>
public class MyListRepository : IMyListRepository
{
    private readonly NetflixCloneDbContext _context;

    public MyListRepository(NetflixCloneDbContext context)
    {
        _context = context;
    }

    public async Task<List<MyListItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.MyListItems
            .Include(ml => ml.Movie)
                .ThenInclude(m => m.MovieGenres)
            .Where(ml => ml.UserId == userId)
            .OrderByDescending(ml => ml.AddedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<MyListItem?> GetByUserAndMovieAsync(Guid userId, Guid movieId, CancellationToken cancellationToken = default)
    {
        return await _context.MyListItems
            .FirstOrDefaultAsync(ml => ml.UserId == userId && ml.MovieId == movieId, cancellationToken);
    }

    public async Task<MyListItem> AddAsync(MyListItem myListItem, CancellationToken cancellationToken = default)
    {
        await _context.MyListItems.AddAsync(myListItem, cancellationToken);
        return myListItem;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _context.MyListItems.FindAsync(new object[] { id }, cancellationToken);
        if (item != null)
        {
            item.IsDeleted = true; // Soft delete
            _context.MyListItems.Update(item);
        }
    }

    public async Task<bool> IsInListAsync(Guid userId, Guid movieId, CancellationToken cancellationToken = default)
    {
        return await _context.MyListItems
            .AnyAsync(ml => ml.UserId == userId && ml.MovieId == movieId, cancellationToken);
    }
}
