using Application.Common.Mappings;
using PoolTools.Pool.API.Domain.Entities;

namespace Application.Pools.Queries.GetPools;

public class PoolOptionsDto : IMapFrom<PoolOptions>
{
    public decimal MaximumCap { get; set; }

    public int RosterSize { get; set; }
    public int RequiredForwards { get; set; }
    public int RequiredDefencemen { get; set; }
    public int RequiredGoaltenders { get; set; }
}