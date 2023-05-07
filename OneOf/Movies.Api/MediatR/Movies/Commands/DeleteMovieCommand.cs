using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Persistence;
using OneOf;
using OneOf.Types;

namespace Movies.Api.MediatR.Movies.Commands;

internal sealed class DeleteMovieCommand : IRequest<OneOf<Success, NotFound>>
{
    public DeleteMovieCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

internal sealed class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, OneOf<Success, NotFound>>
{
    private readonly ApplicationDbContext _context;

    public DeleteMovieCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OneOf<Success, NotFound>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _context.Movies.Where(movie => movie.Id == request.Id).ExecuteDeleteAsync().ConfigureAwait(false);

        if (deleted < 1)
        {
            return new NotFound();
        }

        return new Success();
    }
}
