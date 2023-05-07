using Microsoft.EntityFrameworkCore;
using Movies.Api.Domain;

namespace Movies.Api.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync().ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Movies.Any())
        {
            await _context.Movies.AddRangeAsync(
                new Movie()
                {
                    Title = "The Irishman",
                    ReleaseYear = 2019,
                    AgeRestriction = 18,
                    RottenTomatoesPercentage = 98
                },
                new Movie()
                {
                    Title = "Dangal",
                    ReleaseYear = 2016,
                    AgeRestriction = 7,
                    RottenTomatoesPercentage = 97
                },
                new Movie()
                {
                    Title = "David Attenborough: A Life on Our Planet",
                    ReleaseYear = 2020,
                    AgeRestriction = 7,
                    RottenTomatoesPercentage = 95
                }
            ).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
