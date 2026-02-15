using MediatR;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Application.Commands;

/// <summary>
/// Command to create a new movie
/// Used by admins to upload content
/// </summary>
public class CreateMovieCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DurationMinutes { get; set; }
    public string VideoUrl { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string BannerUrl { get; set; } = string.Empty;
    public ContentRating Rating { get; set; }
    public string Director { get; set; } = string.Empty;
    public string Cast { get; set; } = string.Empty;
    public List<Genre> Genres { get; set; } = new();
    public Guid UploadedByUserId { get; set; }
}
