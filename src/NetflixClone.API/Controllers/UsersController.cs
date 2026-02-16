using Microsoft.AspNetCore.Mvc;
using NetflixClone.Domain.Entities;
using NetflixClone.Infrastructure.Data;

namespace NetflixClone.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly NetflixCloneDbContext _context;
    private readonly ILogger<UsersController> _logger;

    public UsersController(NetflixCloneDbContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Create a test user (for development/testing)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        _logger.LogInformation("Creating user: {Email}", request.Email);

        var user = new User
        {
            Id = request.UserId ?? Guid.NewGuid(),
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            ProfilePictureUrl = request.ProfilePictureUrl ?? string.Empty,
            SubscriptionTier = request.SubscriptionTier ?? "Free",
            LastLoginAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { id = user.Id, email = user.Email, message = "User created successfully" });
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        
        if (user == null)
        {
            return NotFound(new { message = $"User with ID {id} not found" });
        }

        return Ok(new
        {
            id = user.Id,
            email = user.Email,
            firstName = user.FirstName,
            lastName = user.LastName,
            subscriptionTier = user.SubscriptionTier
        });
    }

    /// <summary>
    /// Create the default test user
    /// </summary>
    [HttpPost("create-test-user")]
    public async Task<IActionResult> CreateTestUser()
    {
        var testUserId = new Guid("00000000-0000-0000-0000-000000000001");

        // Check if already exists
        var existing = await _context.Users.FindAsync(testUserId);
        if (existing != null)
        {
            return Ok(new { message = "Test user already exists", id = testUserId });
        }

        var user = new User
        {
            Id = testUserId,
            Email = "test@netflixclone.com",
            FirstName = "Test",
            LastName = "User",
            ProfilePictureUrl = "https://example.com/avatar.jpg",
            SubscriptionTier = "Free",
            LastLoginAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { 
            message = "Test user created successfully", 
            id = user.Id,
            email = user.Email
        });
    }
}

/// <summary>
/// Request model for creating a user
/// </summary>
public class CreateUserRequest
{
    public Guid? UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public string? SubscriptionTier { get; set; }
}
