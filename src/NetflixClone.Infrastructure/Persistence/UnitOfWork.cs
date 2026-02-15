using Microsoft.EntityFrameworkCore.Storage;
using NetflixClone.Application.Interfaces;
using NetflixClone.Infrastructure.Data;
using NetflixClone.Infrastructure.Repositories;

namespace NetflixClone.Infrastructure.Persistence;

/// <summary>
/// Implementation of Unit of Work pattern
/// Manages transactions across multiple repositories
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly NetflixCloneDbContext _context;
    private IDbContextTransaction? _transaction;

    // Lazy-loaded repositories
    private IMovieRepository? _movies;
    private IWatchHistoryRepository? _watchHistories;
    private IMyListRepository? _myLists;
    private IRoomRepository? _rooms;

    public UnitOfWork(NetflixCloneDbContext context)
    {
        _context = context;
    }

    public IMovieRepository Movies => 
        _movies ??= new MovieRepository(_context);

    public IWatchHistoryRepository WatchHistories => 
        _watchHistories ??= new WatchHistoryRepository(_context);

    public IMyListRepository MyLists => 
        _myLists ??= new MyListRepository(_context);

    public IRoomRepository Rooms => 
        _rooms ??= new RoomRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
