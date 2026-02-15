using NetflixClone.Domain.Common;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Domain.Entities;

/// <summary>
/// Movie entity representing a video content in the system
/// Core aggregate root for movie-related operations
/// </summary>
public class Movie : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DurationMinutes { get; set; }
    
    /// <summary>
    /// URL or path to the video file (Azure Blob or local storage)
    /// </summary>
    public string VideoUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// URL or path to the thumbnail image
    /// </summary>
    public string ThumbnailUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// URL or path to the banner/backdrop image
    /// </summary>
    public string BannerUrl { get; set; } = string.Empty;
    
    public ContentRating Rating { get; set; }
    
    /// <summary>
    /// Average rating from users (0-10)
    /// </summary>
    public decimal AverageRating { get; set; }
    
    /// <summary>
    /// Popularity score (calculated by background job based on views, ratings, etc.)
    /// </summary>
    public int PopularityScore { get; set; }
    
    /// <summary>
    /// Total number of views
    /// </summary>
    public long ViewCount { get; set; }
    
    /// <summary>
    /// Director name
    /// </summary>
    public string Director { get; set; } = string.Empty;
    
    /// <summary>
    /// Comma-separated cast members
    /// </summary>
    public string Cast { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
    public ICollection<MyListItem> MyListItems { get; set; } = new List<MyListItem>();
}
