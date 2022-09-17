using Microsoft.EntityFrameworkCore;
using PoolTools.Pool.API.Domain.Entities;

namespace Application.Common.Interfaces;
public interface IApplicationContext
{
    DbSet<Pool> Pools { get; }
    DbSet<PoolTeam> PoolTeams { get; }
    DbSet<Player> Players { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
