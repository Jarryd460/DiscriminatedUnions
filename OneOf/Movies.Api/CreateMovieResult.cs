using Movies.Api.Errors;
using Movies.Api.Validation;
using OneOf;
using OneOf.Types;

namespace Movies.Api;

internal sealed class CreateMovieResult : OneOfBase<Success, MovieCreateError, ValidationFailed>
{
    public CreateMovieResult(OneOf<Success, MovieCreateError, ValidationFailed> input) : base(input)
    {
    }

    public static implicit operator CreateMovieResult(Success success) => new(success);

    public static implicit operator CreateMovieResult(MovieCreateError movieCreateError) => new(movieCreateError);

    public static implicit operator CreateMovieResult(ValidationFailed validationFailed) => new(validationFailed);
}
