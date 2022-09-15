using Application.Common.Mappings;
using PoolTools.Pool.API.Domain.ValueObjects;

namespace Application.PoolTeams.Queries;

public sealed class DraftPickDto : IMapFrom<DraftPick>
{
    public int Round { get; set; }
    public int Position { get; set; }
}
