using NetflixClone.Domain.Entities;

namespace NetflixClone.Application.Interfaces;

/// <summary>
/// Repository interface for WatchHistory entity operations
/// </summary>
public interface IWatchHistoryRepository
{
    Task<WatchHistory?> GetByUserAndMovieAsync(Guid userId, Guid movieId, CancellationToken cancellationToken = default);
    Task<List<WatchHistory>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<WatchHistory>> GetContinueWatchingAsync(Guid userId, int count, CancellationToken cancellationToken = default);
    Task<WatchHistory> AddAsync(WatchHistory watchHistory, CancellationToken cancellationToken = default);
    Task UpdateAsync(WatchHistory watchHistory, CancellationToken cancellationToken = default);
}
