using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetflixClone.Application.Commands;

namespace NetflixClone.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(IMediator mediator, ILogger<RoomsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Create a new watch party room
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
    {
        _logger.LogInformation("Creating watch party room for movie {MovieId}", request.MovieId);

        var command = new CreateRoomCommand(request.Name, request.HostUserId, request.MovieId);
        var roomCode = await _mediator.Send(command);

        return Ok(new { roomCode, message = "Room created successfully" });
    }

    /// <summary>
    /// Get room details by room code
    /// </summary>
    [HttpGet("{code}")]
    public async Task<IActionResult> GetRoomByCode(string code)
    {
        _logger.LogInformation("Getting room with code: {RoomCode}", code);
        
        // Note: We'll need to create GetRoomByCodeQuery in Application layer
        // For now, return a placeholder
        return Ok(new { roomCode = code, message = "Room endpoint - to be implemented with SignalR" });
    }
}

/// <summary>
/// Request model for creating a room
/// </summary>
public class CreateRoomRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid HostUserId { get; set; }
    public Guid MovieId { get; set; }
}
