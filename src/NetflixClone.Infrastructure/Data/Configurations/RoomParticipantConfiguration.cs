using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for RoomParticipant
/// </summary>
public class RoomParticipantConfiguration : IEntityTypeConfiguration<RoomParticipant>
{
    public void Configure(EntityTypeBuilder<RoomParticipant> builder)
    {
        builder.ToTable("RoomParticipants");

        builder.HasKey(rp => rp.Id);

        // Composite index
        builder.HasIndex(rp => new { rp.RoomId, rp.UserId });
        builder.HasIndex(rp => rp.IsConnected);

        // Relationships configured in Room and User configurations
    }
}
