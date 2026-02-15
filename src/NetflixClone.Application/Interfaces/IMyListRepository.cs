using NetflixClone.Domain.Entities;

namespace NetflixClone.Application.Interfaces;

/// <summary>
/// Repository interface for MyList (favorites) operations
/// </summary>
public interface IMyListRepository
{
    Task<List<MyListItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<MyListItem?> GetByUserAndMovieAsync(Guid userId, Guid movieId, CancellationToken cancellationToken = default);
    Task<MyListItem> AddAsync(MyListItem myListItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsInListAsync(Guid userId, Guid movieId, CancellationToken cancellationToken = default);
}
