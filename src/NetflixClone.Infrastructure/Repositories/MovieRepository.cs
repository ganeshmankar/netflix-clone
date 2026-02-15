using Microsoft.EntityFrameworkCore;
using NetflixClone.Application.Interfaces;
using NetflixClone.Domain.Entities;
using NetflixClone.Domain.Enums;
using NetflixClone.Infrastructure.Data;

namespace NetflixClone.Infrastructure.Repositories;

/// <summary>
/// Implementation of IMovieRepository
/// Handles all movie data access operations
/// </summary>
public class MovieRepository : IMovieRepository
{
    private readonly NetflixCloneDbContext _context;

    public MovieRepository(NetflixCloneDbContext context)
    {
        _context = context;
    }

    public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Include(m => m.MovieGenres)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Include(m => m.MovieGenres)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Movie>> GetByGenreAsync(Genre genre, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Include(m => m.MovieGenres)
            .Where(m => m.MovieGenres.Any(mg => mg.Genre == genre))
            .OrderByDescending(m => m.PopularityScore)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Movie>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        var lowerSearchTerm = searchTerm.ToLower();

        return await _context.Movies
            .Include(m => m.MovieGenres)
            .Where(m => 
                m.Title.ToLower().Contains(lowerSearchTerm) ||
                m.Description.ToLower().Contains(lowerSearchTerm) ||
                m.Director.ToLower().Contains(lowerSearchTerm) ||
                m.Cast.ToLower().Contains(lowerSearchTerm))
            .OrderByDescending(m => m.PopularityScore)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Movie>> GetPopularAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Include(m => m.MovieGenres)
            .OrderByDescending(m => m.PopularityScore)
            .ThenByDescending(m => m.ViewCount)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Movie>> GetRecentAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Include(m => m.MovieGenres)
            .OrderByDescending(m => m.CreatedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<Movie> AddAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        await _context.Movies.AddAsync(movie, cancellationToken);
        return movie;
    }

    public async Task UpdateAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        _context.Movies.Update(movie);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var movie = await GetByIdAsync(id, cancellationToken);
        if (movie != null)
        {
            movie.IsDeleted = true; // Soft delete
            _context.Movies.Update(movie);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Movies.AnyAsync(m => m.Id == id, cancellationToken);
    }
}
