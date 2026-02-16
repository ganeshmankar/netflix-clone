using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetflixClone.Application.Commands;
using NetflixClone.Application.Queries;

namespace NetflixClone.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MyListController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MyListController> _logger;

    public MyListController(IMediator mediator, ILogger<MyListController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get user's favorite movies list
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetMyList([FromQuery] Guid userId)
    {
        _logger.LogInformation("Getting My List for user: {UserId}", userId);
        var query = new GetMyListQuery(userId);
        var movies = await _mediator.Send(query);
        return Ok(movies);
    }

    /// <summary>
    /// Add movie to user's favorites
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddToMyList([FromBody] AddToMyListRequest request)
    {
        _logger.LogInformation("Adding movie {MovieId} to My List for user {UserId}", 
            request.MovieId, request.UserId);

        var command = new AddToMyListCommand(request.UserId, request.MovieId);
        var listItemId = await _mediator.Send(command);

        return Ok(new { id = listItemId, message = "Movie added to My List" });
    }

    /// <summary>
    /// Remove movie from user's favorites
    /// </summary>
    [HttpDelete("{movieId}")]
    public async Task<IActionResult> RemoveFromMyList(Guid movieId, [FromQuery] Guid userId)
    {
        _logger.LogInformation("Removing movie {MovieId} from My List for user {UserId}", 
            movieId, userId);

        var command = new RemoveFromMyListCommand(userId, movieId);
        await _mediator.Send(command);

        return Ok(new { message = "Movie removed from My List" });
    }
}

/// <summary>
/// Request model for adding to My List
/// </summary>
public class AddToMyListRequest
{
    public Guid UserId { get; set; }
    public Guid MovieId { get; set; }
}
