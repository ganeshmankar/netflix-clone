namespace NetflixClone.Application.Interfaces;

/// <summary>
/// Unit of Work pattern interface
/// Manages transactions across multiple repositories
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IMovieRepository Movies { get; }
    IWatchHistoryRepository WatchHistories { get; }
    IMyListRepository MyLists { get; }
    IRoomRepository Rooms { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
