using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetflixClone.Application.Commands;
using NetflixClone.Application.Queries;

namespace NetflixClone.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(IMediator mediator, ILogger<MoviesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all movies
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        _logger.LogInformation("Getting all movies");
        var query = new GetMoviesQuery();
        var movies = await _mediator.Send(query);
        return Ok(movies);
    }

    /// <summary>
    /// Get movie by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(Guid id)
    {
        _logger.LogInformation("Getting movie with ID: {MovieId}", id);
        var query = new GetMovieByIdQuery(id);
        var movie = await _mediator.Send(query);

        if (movie == null)
        {
            return NotFound(new { message = $"Movie with ID {id} not found" });
        }

        return Ok(movie);
    }

    /// <summary>
    /// Search movies by title, director, cast, or description
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> SearchMovies([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return BadRequest(new { message = "Search term is required" });
        }

        _logger.LogInformation("Searching movies with term: {SearchTerm}", term);
        var query = new SearchMoviesQuery(term);
        var movies = await _mediator.Send(query);
        return Ok(movies);
    }

    /// <summary>
    /// Get popular/trending movies
    /// </summary>
    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies([FromQuery] int count = 20)
    {
        _logger.LogInformation("Getting {Count} popular movies", count);
        var query = new GetPopularMoviesQuery(count);
        var movies = await _mediator.Send(query);
        return Ok(movies);
    }

    /// <summary>
    /// Create a new movie (Admin only - will add authorization later)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieCommand command)
    {
        _logger.LogInformation("Creating new movie: {Title}", command.Title);
        var movieId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMovieById), new { id = movieId }, new { id = movieId });
    }
}
