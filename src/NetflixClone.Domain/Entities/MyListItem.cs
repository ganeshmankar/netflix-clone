using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.Entities;

/// <summary>
/// User's personal list of favorite/bookmarked movies
/// Implements the "My List" feature
/// </summary>
public class MyListItem : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    
    /// <summary>
    /// When the movie was added to the list
    /// </summary>
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
