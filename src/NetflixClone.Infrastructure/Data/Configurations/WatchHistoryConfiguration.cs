using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for WatchHistory
/// </summary>
public class WatchHistoryConfiguration : IEntityTypeConfiguration<WatchHistory>
{
    public void Configure(EntityTypeBuilder<WatchHistory> builder)
    {
        builder.ToTable("WatchHistories");

        builder.HasKey(wh => wh.Id);

        builder.Property(wh => wh.PercentageWatched)
            .HasPrecision(5, 2); // e.g., 95.50

        // Composite index for quick lookups
        builder.HasIndex(wh => new { wh.UserId, wh.MovieId }).IsUnique();
        builder.HasIndex(wh => wh.LastWatchedAt);

        // Relationships configured in User and Movie configurations
    }
}
