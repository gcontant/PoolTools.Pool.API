using Application.Common.Mappings;
using PoolTools.Pool.API.Domain.Entities;

namespace Application.Pools.Queries.GetPools;

public class PoolDto : IMapFrom<Pool>
{
    public string Name { get; set; } = string.Empty;
    public IList<PoolTeamDto> Teams { get; set; } = new List<PoolTeamDto>();
}
