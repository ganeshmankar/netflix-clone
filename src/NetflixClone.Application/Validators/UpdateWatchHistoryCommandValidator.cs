using FluentValidation;
using NetflixClone.Application.Commands;

namespace NetflixClone.Application.Validators;

/// <summary>
/// Validator for UpdateWatchHistoryCommand
/// Ensures watch history data is valid
/// </summary>
public class UpdateWatchHistoryCommandValidator : AbstractValidator<UpdateWatchHistoryCommand>
{
    public UpdateWatchHistoryCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.MovieId)
            .NotEmpty().WithMessage("Movie ID is required");

        RuleFor(x => x.LastWatchedPositionSeconds)
            .GreaterThanOrEqualTo(0).WithMessage("Watch position cannot be negative");

        RuleFor(x => x.MovieDurationSeconds)
            .GreaterThan(0).WithMessage("Movie duration must be greater than 0");

        RuleFor(x => x)
            .Must(x => x.LastWatchedPositionSeconds <= x.MovieDurationSeconds)
            .WithMessage("Watch position cannot exceed movie duration");
    }
}
