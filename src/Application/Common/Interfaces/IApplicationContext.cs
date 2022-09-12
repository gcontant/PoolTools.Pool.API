using Microsoft.EntityFrameworkCore;
using PoolTools.Pool.API.Domain.Entities;

namespace Application.Common.Interfaces;
public interface IApplicationContext
{
    DbSet<Pool> Pools { get; }

    Task SaveChanges(CancellationToken cancellationToken);
}
