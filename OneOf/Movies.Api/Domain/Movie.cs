namespace Movies.Api.Domain;
public sealed class Movie
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public int ReleaseYear { get; set; }

    public int AgeRestriction { get; set; }

    public int RottenTomatoesPercentage { get; set; }
}
