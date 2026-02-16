using MediatR;
using NetflixClone.Application.Commands;
using NetflixClone.Application.Interfaces;

namespace NetflixClone.Application.Handlers;

/// <summary>
/// Handler for removing movie from My List
/// </summary>
public class RemoveFromMyListCommandHandler : IRequestHandler<RemoveFromMyListCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveFromMyListCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveFromMyListCommand request, CancellationToken cancellationToken)
    {
        // Find the list item
        var listItem = await _unitOfWork.MyLists.GetByUserAndMovieAsync(
            request.UserId, 
            request.MovieId, 
            cancellationToken);

        if (listItem == null)
        {
            throw new InvalidOperationException("Movie not found in your list");
        }

        // Remove from list (soft delete)
        await _unitOfWork.MyLists.DeleteAsync(listItem.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
