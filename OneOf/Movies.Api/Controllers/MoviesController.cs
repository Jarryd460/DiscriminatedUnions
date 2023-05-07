using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Domain;
using Movies.Api.MediatR.Movies.Commands;
using Movies.Api.MediatR.Movies.Queries;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetMovies")]
        public async Task<IEnumerable<Movie>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMoviesQuery(), cancellationToken).ConfigureAwait(false);

            return result.Match(
                movies => movies
            );
        }

        [HttpGet("{id:guid}", Name = "GetMovie")]
        public async Task<ActionResult<Movie>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMovieQuery(id), cancellationToken).ConfigureAwait(false);

            return result.Match<ActionResult<Movie>>(
                movie => movie,
                _ => NotFound()
            );
        }

        [HttpPost("CreateMovie")]
        public async Task<ActionResult<Movie>> Create([FromBody] Movie movie)
        {
            var result = await _mediator.Send(new CreateMovieCommand(movie)).ConfigureAwait(false);

            return result.Match<ActionResult<Movie>>(
                _ => CreatedAtAction("Get", new { movie.Id }, movie),
                _ => Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Unknown error attempting to save movie"),
                validationProblem => BadRequest(validationProblem)
            );
        }

        [HttpPut("movies/{id:guid}", Name = "UpdateMovie")]
        public async Task<ActionResult<Movie>> Update([FromRoute] Guid id, [FromBody] Movie updatedMovie)
        {
            var result = await _mediator.Send(new UpdateMovieCommand(id, updatedMovie)).ConfigureAwait(false);

            return result.Match<ActionResult<Movie>>(
                movie => Ok(movie),
                _ => NotFound()
            );
        }

        [HttpDelete("movies/{id:guid}", Name = "DeleteMovie")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteMovieCommand(id)).ConfigureAwait(false);

            return result.Match<IActionResult>(
                _ => Ok(),
                _ => NotFound()
            );
        }
    }
}