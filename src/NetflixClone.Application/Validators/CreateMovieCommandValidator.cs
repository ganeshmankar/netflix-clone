using FluentValidation;
using NetflixClone.Application.Commands;

namespace NetflixClone.Application.Validators;

/// <summary>
/// Validator for CreateMovieCommand
/// Ensures movie data is valid before processing
/// </summary>
public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

        RuleFor(x => x.ReleaseYear)
            .GreaterThan(1800).WithMessage("Release year must be after 1800")
            .LessThanOrEqualTo(DateTime.Now.Year + 2).WithMessage("Release year cannot be more than 2 years in the future");

        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than 0")
            .LessThanOrEqualTo(1000).WithMessage("Duration cannot exceed 1000 minutes");

        RuleFor(x => x.VideoUrl)
            .NotEmpty().WithMessage("Video URL is required");

        RuleFor(x => x.ThumbnailUrl)
            .NotEmpty().WithMessage("Thumbnail URL is required");

        RuleFor(x => x.Director)
            .NotEmpty().WithMessage("Director is required")
            .MaximumLength(200).WithMessage("Director name cannot exceed 200 characters");

        RuleFor(x => x.Genres)
            .NotEmpty().WithMessage("At least one genre is required");

        RuleFor(x => x.UploadedByUserId)
            .NotEmpty().WithMessage("Uploader ID is required");
    }
}
