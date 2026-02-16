using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetflixClone.Application.Commands;
using NetflixClone.Application.Queries;

namespace NetflixClone.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WatchHistoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WatchHistoryController> _logger;

    public WatchHistoryController(IMediator mediator, ILogger<WatchHistoryController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get continue watching list for a user
    /// </summary>
    [HttpGet("continue-watching")]
    public async Task<IActionResult> GetContinueWatching([FromQuery] Guid userId, [FromQuery] int count = 10)
    {
        _logger.LogInformation("Getting continue watching for user: {UserId}", userId);
        var query = new GetContinueWatchingQuery(userId, count);
        var watchHistory = await _mediator.Send(query);
        return Ok(watchHistory);
    }

    /// <summary>
    /// Update watch history (track viewing progress)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> UpdateWatchHistory([FromBody] UpdateWatchHistoryRequest request)
    {
        _logger.LogInformation("Updating watch history for user {UserId}, movie {MovieId}", 
            request.UserId, request.MovieId);

        var command = new UpdateWatchHistoryCommand(
            request.UserId,
            request.MovieId,
            request.LastWatchedPositionSeconds,
            request.MovieDurationSeconds
        );

        await _mediator.Send(command);
        return Ok(new { message = "Watch history updated successfully" });
    }
}

/// <summary>
/// Request model for updating watch history
/// </summary>
public class UpdateWatchHistoryRequest
{
    public Guid UserId { get; set; }
    public Guid MovieId { get; set; }
    public int LastWatchedPositionSeconds { get; set; }
    public int MovieDurationSeconds { get; set; }
}
