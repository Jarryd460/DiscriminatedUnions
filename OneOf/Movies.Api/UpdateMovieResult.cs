using Movies.Api.Domain;
using OneOf;
using OneOf.Types;

namespace Movies.Api;

[GenerateOneOf]
internal sealed partial class UpdateMovieResult : OneOfBase<Movie, NotFound>
{
}
