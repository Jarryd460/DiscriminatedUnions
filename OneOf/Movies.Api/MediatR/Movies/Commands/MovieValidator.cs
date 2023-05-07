using FluentValidation;
using Movies.Api.Domain;

namespace Movies.Api.MediatR.Movies.Commands;

public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(movie => movie.Id).NotEmpty();
        RuleFor(movie => movie.Title).NotEmpty();
        RuleFor(movie => movie.AgeRestriction).GreaterThanOrEqualTo(0);
        RuleFor(movie => movie.RottenTomatoesPercentage).GreaterThanOrEqualTo(0);
        RuleFor(movie => movie.ReleaseYear).LessThanOrEqualTo(DateTime.UtcNow.Year);
    }
}
