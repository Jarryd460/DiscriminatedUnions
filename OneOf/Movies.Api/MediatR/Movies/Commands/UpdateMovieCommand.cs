using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Domain;
using Movies.Api.Persistence;
using OneOf.Types;

namespace Movies.Api.MediatR.Movies.Commands;

internal sealed class UpdateMovieCommand : IRequest<UpdateMovieResult>
{
    public UpdateMovieCommand(Guid id, Movie movie)
    {
        Id = id;
        Movie = movie;
    }

    public Guid Id { get; }
    public Movie Movie { get; }
}

internal sealed class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, UpdateMovieResult>
{
    private readonly ApplicationDbContext _context;

    public UpdateMovieCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateMovieResult> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _context.Movies.SingleOrDefaultAsync(movie => movie.Id == request.Id).ConfigureAwait(false);

        if (existingCustomer is null)
        {
            return new NotFound();
        }

        await _context.Movies.Where(movie => movie.Id == request.Id).ExecuteUpdateAsync(movie => movie.SetProperty(movie => movie.ReleaseYear, movie => request.Movie.ReleaseYear)).ConfigureAwait(false);

        return request.Movie;
    }
}
