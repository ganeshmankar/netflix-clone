using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for MyListItem
/// </summary>
public class MyListItemConfiguration : IEntityTypeConfiguration<MyListItem>
{
    public void Configure(EntityTypeBuilder<MyListItem> builder)
    {
        builder.ToTable("MyListItems");

        builder.HasKey(ml => ml.Id);

        // Composite index to prevent duplicates
        builder.HasIndex(ml => new { ml.UserId, ml.MovieId }).IsUnique();
        builder.HasIndex(ml => ml.AddedAt);

        // Relationships configured in User and Movie configurations
    }
}
