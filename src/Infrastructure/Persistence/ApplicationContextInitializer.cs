using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PoolTools.Pool.API.Domain.Entities;
using PoolTools.Pool.API.Domain.ValueObjects;

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
                new PoolTeam("All-Blacks","Julien B", new List<DraftPick> { DraftPick.From(2, 6), DraftPick.From(3, 8), DraftPick.From(4, 9), DraftPick.From(5, 4), DraftPick.From(5,9) }),
                new PoolTeam("Expos","Peter A", new List<DraftPick> { DraftPick.From(1, 10), DraftPick.From(4, 1), DraftPick.From(4, 3), DraftPick.From(4, 4), DraftPick.From(5,1) }),
                new PoolTeam("Golden Seals","Julien P", new List<DraftPick> { DraftPick.From(2, 8), DraftPick.From(3, 5), DraftPick.From(3, 7), DraftPick.From(4, 8), DraftPick.From(5,8) }),
                new PoolTeam("Marrons","Marc-Etienne B", new List<DraftPick> { DraftPick.From(1, 6), DraftPick.From(1, 8), DraftPick.From(1, 9), DraftPick.From(2, 12), DraftPick.From(4,6) }),
                new PoolTeam("Machine","Jean-Daniel B", new List < DraftPick > { DraftPick.From(3, 9), DraftPick.From(4, 10), DraftPick.From(5, 3), DraftPick.From(5, 6), DraftPick.From(5,10) }),
                new PoolTeam("Nordiques","Jean-Daniel C", new List < DraftPick > { DraftPick.From(1, 11), DraftPick.From(2, 1), DraftPick.From(3, 1), DraftPick.From(3, 3), DraftPick.From(3,10) }),
                new PoolTeam("Patriots","Jeremy T", new List < DraftPick > { DraftPick.From(1, 7), DraftPick.From(2, 7), DraftPick.From(3, 6), DraftPick.From(4, 7), DraftPick.From(5,7) }),
                new PoolTeam("Roadrunners","Gabriel C", new List < DraftPick > { DraftPick.From(1, 5), DraftPick.From(2, 5), DraftPick.From(2, 13), DraftPick.From(3, 4), DraftPick.From(5,5) }),
                new PoolTeam("Rangers","David S", new List < DraftPick > { DraftPick.From(1, 2), DraftPick.From(2, 2), DraftPick.From(3, 2), DraftPick.From(4, 2), DraftPick.From(5,2) }),
                new PoolTeam("ReHabs","Sylvestre R", new List < DraftPick > { DraftPick.From(1, 12), DraftPick.From(2, 3), DraftPick.From(2, 11), DraftPick.From(5, 11), DraftPick.From(5,12) }),
                new PoolTeam("Rouge&Or","Steve G", new List < DraftPick > { DraftPick.From(1, 4), DraftPick.From(2, 4), DraftPick.From(2,9), DraftPick.From(2,10), DraftPick.From(4,11) }),
                new PoolTeam("Saints","Maxime T", new List < DraftPick > { DraftPick.From(1, 1), DraftPick.From(1, 3), DraftPick.From(3, 11), DraftPick.From(4, 5), DraftPick.From(4,12) })
            };

            _context.PoolTeams.AddRange(teams);

            var pool = new Pool("LigueQM");
            foreach (var team in teams)
            {
                pool.AddTeam(team);
            }

            _context.Pools.Add(pool);

            var poolOptions = new PoolOptions
            {
                Pool = pool,
                MaximumCap = 82500000,
                RosterSize = 20,
                RequiredForwards = 9,
                RequiredDefencemen = 4,
                RequiredGoaltenders = 1
            };
            _context.PoolOptions.Add(poolOptions);

            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
