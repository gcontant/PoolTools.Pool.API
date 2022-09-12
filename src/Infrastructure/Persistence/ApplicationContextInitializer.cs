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
                new PoolTeam(new TeamId(1),"All-Blacks","Julien B"),
                new PoolTeam(new TeamId(2),"Expos","Peter A"),
                new PoolTeam(new TeamId(3),"Golden Seals","Julien P"),
                new PoolTeam(new TeamId(4),"Marrons","Marc-Etienne B"),
                new PoolTeam(new TeamId(5), "Machine","Jean-Daniel B"),
                new PoolTeam(new TeamId(6), "Nordiques","Jean-Daniel C"),
                new PoolTeam(new TeamId(7), "Patriots","Jeremy T"),
                new PoolTeam(new TeamId(8), "Roadrunners","Gabriel C"),
                new PoolTeam(new TeamId(9), "Rangers","David S"),
                new PoolTeam(new TeamId(10), "ReHabs","Sylvestre R"),
                new PoolTeam(new TeamId(11), "Rouge&Or","Steve G"),
                new PoolTeam(new TeamId(12), "Saints","Maxime T")
            };

            _context.PoolTeams.AddRange(teams);

            var pool = new Pool(new PoolId(1),"LigueQM")
            {
                Teams = teams
            };

            _context.Pools.Add(pool);

            await _context.SaveChangesAsync();
        }
    }
}
