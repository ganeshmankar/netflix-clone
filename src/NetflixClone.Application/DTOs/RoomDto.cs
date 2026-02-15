namespace NetflixClone.Application.DTOs;

/// <summary>
/// Watch party room information
/// </summary>
public class RoomDto
{
    public Guid Id { get; set; }
    public string RoomCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid HostUserId { get; set; }
    public Guid MovieId { get; set; }
    public MovieDto Movie { get; set; } = null!;
    public int CurrentPositionSeconds { get; set; }
    public bool IsPlaying { get; set; }
    public bool IsActive { get; set; }
    public List<RoomParticipantDto> Participants { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
