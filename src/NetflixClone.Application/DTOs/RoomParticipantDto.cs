namespace NetflixClone.Application.DTOs;

/// <summary>
/// Participant in a watch party room
/// </summary>
public class RoomParticipantDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public bool IsConnected { get; set; }
    public DateTime JoinedAt { get; set; }
}
