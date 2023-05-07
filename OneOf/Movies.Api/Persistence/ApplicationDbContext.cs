using Microsoft.EntityFrameworkCore;
using Movies.Api.Domain;

namespace Movies.Api.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies => Set<Movie>();
}
