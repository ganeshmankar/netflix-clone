using MediatR;
using NetflixClone.Application.Commands;
using NetflixClone.Application.Interfaces;
using NetflixClone.Domain.Entities;
using NetflixClone.Domain.Events;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for AddToMyListCommand
/// Adds a movie to user's favorites and raises domain event
/// </summary>
public class AddToMyListCommandHandler : IRequestHandler<AddToMyListCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public AddToMyListCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(AddToMyListCommand request, CancellationToken cancellationToken)
    {
        // Check if already in list
        var existing = await _unitOfWork.MyLists.GetByUserAndMovieAsync(
            request.UserId,
            request.MovieId,
            cancellationToken
        );

        if (existing != null)
        {
            // Already in list, return existing ID
            return existing.Id;
        }

        // Check if movie exists
        var movieExists = await _unitOfWork.Movies.ExistsAsync(request.MovieId, cancellationToken);
        if (!movieExists)
        {
            throw new InvalidOperationException($"Movie with ID {request.MovieId} does not exist");
        }

        // Add to my list
        var myListItem = new MyListItem
        {
            UserId = request.UserId,
            MovieId = request.MovieId,
            AddedAt = DateTime.UtcNow
        };

        var created = await _unitOfWork.MyLists.AddAsync(myListItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Raise domain event for recommendations
        var addedEvent = new MovieAddedToListEvent(request.UserId, request.MovieId);
        await _publisher.Publish(addedEvent, cancellationToken);

        return created.Id;
    }
}
