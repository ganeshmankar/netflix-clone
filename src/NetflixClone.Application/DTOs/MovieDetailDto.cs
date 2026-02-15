using NetflixClone.Domain.Enums;

namespace NetflixClone.Application.DTOs;

/// <summary>
/// Detailed movie information for movie details page
/// Includes all information: video URL, cast, director, etc.
/// </summary>
public class MovieDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DurationMinutes { get; set; }
    public string VideoUrl { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string BannerUrl { get; set; } = string.Empty;
    public ContentRating Rating { get; set; }
    public decimal AverageRating { get; set; }
    public int PopularityScore { get; set; }
    public long ViewCount { get; set; }
    public string Director { get; set; } = string.Empty;
    public string Cast { get; set; } = string.Empty;
    public List<Genre> Genres { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
