using NetflixClone.Domain.Common;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Domain.Entities;

/// <summary>
/// Junction table for many-to-many relationship between Movie and Genre
/// </summary>
public class MovieGenre : BaseEntity
{
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    
    public Genre Genre { get; set; }
}
