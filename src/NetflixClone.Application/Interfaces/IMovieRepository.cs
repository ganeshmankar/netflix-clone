using NetflixClone.Domain.Entities;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Application.Interfaces;

/// <summary>
/// Repository interface for Movie entity operations
/// Implementation will be in Infrastructure layer
/// </summary>
public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Movie>> GetByGenreAsync(Genre genre, CancellationToken cancellationToken = default);
    Task<List<Movie>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<List<Movie>> GetPopularAsync(int count, CancellationToken cancellationToken = default);
    Task<List<Movie>> GetRecentAsync(int count, CancellationToken cancellationToken = default);
    Task<Movie> AddAsync(Movie movie, CancellationToken cancellationToken = default);
    Task UpdateAsync(Movie movie, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
