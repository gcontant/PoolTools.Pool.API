using Application.Common.Mappings;
using PoolTools.Pool.API.Domain.Entities;

namespace Application.Pools.Queries.GetPools;

public class PoolTeamDto : IMapFrom<PoolTeam>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
}
