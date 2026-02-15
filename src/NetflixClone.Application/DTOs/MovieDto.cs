using NetflixClone.Domain.Enums;

namespace NetflixClone.Application.DTOs;

/// <summary>
/// Basic movie information for list views
/// Used in browse pages, search results, rows
/// </summary>
public class MovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DurationMinutes { get; set; }
    public string ThumbnailUrl { get; set; } = string.Empty;
    public ContentRating Rating { get; set; }
    public decimal AverageRating { get; set; }
    public List<Genre> Genres { get; set; } = new();
}
