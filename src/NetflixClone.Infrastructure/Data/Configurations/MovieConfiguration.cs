using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for Movie entity
/// Defines table schema, indexes, and relationships
/// </summary>
public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(m => m.VideoUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.ThumbnailUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.BannerUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.Director)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Cast)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(m => m.AverageRating)
            .HasPrecision(3, 2); // e.g., 4.75

        // Indexes for performance
        builder.HasIndex(m => m.Title);
        builder.HasIndex(m => m.ReleaseYear);
        builder.HasIndex(m => m.PopularityScore);
        builder.HasIndex(m => m.CreatedAt);

        // Relationships
        builder.HasMany(m => m.MovieGenres)
            .WithOne(mg => mg.Movie)
            .HasForeignKey(mg => mg.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.WatchHistories)
            .WithOne(wh => wh.Movie)
            .HasForeignKey(wh => wh.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.MyListItems)
            .WithOne(ml => ml.Movie)
            .HasForeignKey(ml => ml.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
