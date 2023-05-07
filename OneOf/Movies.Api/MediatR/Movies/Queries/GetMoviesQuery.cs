using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Domain;
using Movies.Api.Persistence;
using OneOf;

namespace Movies.Api.MediatR.Movies.Queries;

internal sealed class GetMoviesQuery : IRequest<OneOf<List<Movie>>>
{
}

internal sealed class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, OneOf<List<Movie>>>
{
    private readonly ApplicationDbContext _context;

    public GetMoviesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OneOf<List<Movie>>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Movies.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}
