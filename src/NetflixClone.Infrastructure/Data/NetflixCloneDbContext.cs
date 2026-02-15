using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetflixClone.Domain.Entities;
using NetflixClone.Infrastructure.Data.Configurations;

namespace NetflixClone.Infrastructure.Data;

/// <summary>
/// Main database context for Netflix Clone
/// Inherits from IdentityDbContext for ASP.NET Identity support
/// </summary>
public class NetflixCloneDbContext : IdentityDbContext
{
    public NetflixCloneDbContext(DbContextOptions<NetflixCloneDbContext> options)
        : base(options)
    {
    }

    // DbSets for domain entities
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<User> Users => Set<User>();
    public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
    public DbSet<WatchHistory> WatchHistories => Set<WatchHistory>();
    public DbSet<MyListItem> MyListItems => Set<MyListItem>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<RoomParticipant> RoomParticipants => Set<RoomParticipant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply entity configurations
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new MovieGenreConfiguration());
        modelBuilder.ApplyConfiguration(new WatchHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new MyListItemConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new RoomParticipantConfiguration());

        // Global query filters for soft delete
        modelBuilder.Entity<Movie>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<WatchHistory>().HasQueryFilter(wh => !wh.IsDeleted);
        modelBuilder.Entity<MyListItem>().HasQueryFilter(ml => !ml.IsDeleted);
        modelBuilder.Entity<Room>().HasQueryFilter(r => !r.IsDeleted);
        modelBuilder.Entity<RoomParticipant>().HasQueryFilter(rp => !rp.IsDeleted);
    }

    /// <summary>
    /// Override SaveChanges to automatically set audit fields
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Domain.Common.BaseEntity && 
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (Domain.Common.BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
