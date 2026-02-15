using MediatR;
using NetflixClone.Application.Commands;
using NetflixClone.Application.Interfaces;
using NetflixClone.Domain.Entities;
using NetflixClone.Domain.Events;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for CreateMovieCommand
/// Creates a new movie and raises domain event
/// </summary>
public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CreateMovieCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        // Create movie entity
        var movie = new Movie
        {
            Title = request.Title,
            Description = request.Description,
            ReleaseYear = request.ReleaseYear,
            DurationMinutes = request.DurationMinutes,
            VideoUrl = request.VideoUrl,
            ThumbnailUrl = request.ThumbnailUrl,
            BannerUrl = request.BannerUrl,
            Rating = request.Rating,
            Director = request.Director,
            Cast = request.Cast,
            AverageRating = 0,
            PopularityScore = 0,
            ViewCount = 0
        };

        // Add genres
        foreach (var genre in request.Genres)
        {
            movie.MovieGenres.Add(new MovieGenre
            {
                Genre = genre,
                Movie = movie
            });
        }

        // Save to database
        var createdMovie = await _unitOfWork.Movies.AddAsync(movie, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Raise domain event
        var movieUploadedEvent = new MovieUploadedEvent(
            createdMovie.Id,
            createdMovie.Title,
            request.UploadedByUserId
        );
        await _publisher.Publish(movieUploadedEvent, cancellationToken);

        return createdMovie.Id;
    }
}
