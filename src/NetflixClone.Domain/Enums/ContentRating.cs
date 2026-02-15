namespace NetflixClone.Domain.Enums;

/// <summary>
/// Content rating for movies (similar to MPAA ratings)
/// </summary>
public enum ContentRating
{
    G = 1,      // General Audiences
    PG = 2,     // Parental Guidance Suggested
    PG13 = 3,   // Parents Strongly Cautioned
    R = 4,      // Restricted
    NC17 = 5,   // Adults Only
    NR = 6      // Not Rated
}
