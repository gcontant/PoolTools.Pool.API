using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PoolTools.Pool.API.Domain.Entities;

namespace Infrastructure.Persistence;

public class ApplicationContextInitializer
{
    private readonly ILogger<ApplicationContextInitializer> _logger;
    private readonly ApplicationContext _context;

    public ApplicationContextInitializer(ApplicationContext context, ILogger<ApplicationContextInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (!_context.Pools.Any())
        {
            var teams = new List<PoolTeam>
            {
                new PoolTeam("All-Blacks","Julien B"),
                new PoolTeam("Expos","Peter A"),
                new PoolTeam("Golden Seals","Julien P"),
                new PoolTeam("Marrons","Marc-Etienne B"),
                new PoolTeam("Machine","Jean-Daniel B"),
                new PoolTeam("Nordiques","Jean-Daniel C"),
                new PoolTeam("Patriots","Jeremy T"),
                new PoolTeam("Roadrunners","Gabriel C"),
                new PoolTeam("Rangers","David S"),
                new PoolTeam("ReHabs","Sylvestre R"),
                new PoolTeam("Rouge&Or","Steve G"),
                new PoolTeam("Saints","Maxime T")
            };

            _context.PoolTeams.AddRange(teams);

            var pool = new Pool("LigueQM")
            {
                Teams = teams
            };

            _context.Pools.Add(pool);

            await _context.SaveChangesAsync();
        }
    }
}
