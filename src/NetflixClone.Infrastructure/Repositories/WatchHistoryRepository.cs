using Microsoft.EntityFrameworkCore;
using NetflixClone.Application.Interfaces;
using NetflixClone.Domain.Entities;
using NetflixClone.Infrastructure.Data;

namespace NetflixClone.Infrastructure.Repositories;

/// <summary>
/// Implementation of IWatchHistoryRepository
/// Handles watch history data access
/// </summary>
public class WatchHistoryRepository : IWatchHistoryRepository
{
    private readonly NetflixCloneDbContext _context;

    public WatchHistoryRepository(NetflixCloneDbContext context)
    {
        _context = context;
    }

    public async Task<WatchHistory?> GetByUserAndMovieAsync(Guid userId, Guid movieId, CancellationToken cancellationToken = default)
    {
        return await _context.WatchHistories
            .Include(wh => wh.Movie)
                .ThenInclude(m => m.MovieGenres)
            .FirstOrDefaultAsync(wh => wh.UserId == userId && wh.MovieId == movieId, cancellationToken);
    }

    public async Task<List<WatchHistory>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.WatchHistories
            .Include(wh => wh.Movie)
                .ThenInclude(m => m.MovieGenres)
            .Where(wh => wh.UserId == userId)
            .OrderByDescending(wh => wh.LastWatchedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<WatchHistory>> GetContinueWatchingAsync(Guid userId, int count, CancellationToken cancellationToken = default)
    {
        return await _context.WatchHistories
            .Include(wh => wh.Movie)
                .ThenInclude(m => m.MovieGenres)
            .Where(wh => wh.UserId == userId && !wh.IsCompleted && wh.PercentageWatched > 0)
            .OrderByDescending(wh => wh.LastWatchedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<WatchHistory> AddAsync(WatchHistory watchHistory, CancellationToken cancellationToken = default)
    {
        await _context.WatchHistories.AddAsync(watchHistory, cancellationToken);
        return watchHistory;
    }

    public async Task UpdateAsync(WatchHistory watchHistory, CancellationToken cancellationToken = default)
    {
        _context.WatchHistories.Update(watchHistory);
        await Task.CompletedTask;
    }
}
