using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for Room (Watch Party)
/// </summary>
public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoomCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Unique room code
        builder.HasIndex(r => r.RoomCode).IsUnique();
        builder.HasIndex(r => r.IsActive);

        // Relationships
        builder.HasMany(r => r.Participants)
            .WithOne(rp => rp.Room)
            .HasForeignKey(rp => rp.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
