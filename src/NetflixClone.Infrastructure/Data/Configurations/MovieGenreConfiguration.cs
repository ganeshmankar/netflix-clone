using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for MovieGenre junction table
/// </summary>
public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("MovieGenres");

        builder.HasKey(mg => mg.Id);

        // Composite index for performance
        builder.HasIndex(mg => new { mg.MovieId, mg.Genre }).IsUnique();

        // Relationships configured in MovieConfiguration
    }
}
