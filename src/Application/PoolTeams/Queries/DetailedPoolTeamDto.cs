using Application.Common.Mappings;
using PoolTools.Pool.API.Domain.Entities;

namespace Application.PoolTeams.Queries;

public class DetailedPoolTeamDto : IMapFrom<PoolTeam>
{
    public string Name { get; set; }
    public string Owner { get; set; }

    public IList<DraftPickDto> DraftPicks { get; set; } = new List<DraftPickDto>();
    public IList<PlayerDto> Roster { get; set; } = new List<PlayerDto>();

    //TODO: Add required by position
    //TODO: Add remaining Cap Space
}
