using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Domain;
using Movies.Api.Persistence;
using OneOf;
using OneOf.Types;

namespace Movies.Api.MediatR.Movies.Queries;

internal sealed class GetMovieQuery : IRequest<OneOf<Movie, None>>
{
    public GetMovieQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

internal sealed class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, OneOf<Movie, None>>
{
    private readonly ApplicationDbContext _context;

    public GetMovieQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OneOf<Movie, None>> Handle(GetMovieQuery request, CancellationToken cancellationToken)
    {

        var movie = await _context.Movies.SingleOrDefaultAsync(movie => movie.Id == request.Id, cancellationToken).ConfigureAwait(false);

        if (movie is null)
        {
            // NotFound should be returned in a real world scenario but to show the different types I've returned None
            return new None();
        }

        return movie;
    }
}