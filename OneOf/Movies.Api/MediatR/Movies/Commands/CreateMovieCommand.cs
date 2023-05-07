using FluentValidation;
using MediatR;
using Movies.Api.Domain;
using Movies.Api.Errors;
using Movies.Api.Persistence;
using Movies.Api.Validation;
using OneOf.Types;

namespace Movies.Api.MediatR.Movies.Commands;

internal sealed class CreateMovieCommand : IRequest<CreateMovieResult>
{
    public CreateMovieCommand(Movie movie)
    {
        Movie = movie;
    }

    public Movie Movie { get; }
}

internal sealed class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, CreateMovieResult>
{
    private readonly ApplicationDbContext _context;
    private readonly IValidator<Movie> _validator;

    public CreateMovieCommandHandler(ApplicationDbContext context, IValidator<Movie> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<CreateMovieResult> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Movie).ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        await _context.Movies.AddAsync(request.Movie).ConfigureAwait(false);

        try
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (Exception)
        {
            return new MovieCreateError();
        }

        return new Success();
    }
}