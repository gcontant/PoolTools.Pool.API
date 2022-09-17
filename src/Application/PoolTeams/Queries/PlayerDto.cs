using Application.Common.Mappings;
using PoolTools.Pool.API.Domain.Entities;

namespace Application.PoolTeams.Queries;

public sealed class PlayerDto : IMapFrom<Player>
{
    public string FullName { get; set; }
    public string Position { get; set; }
    public string TeamCode { get; set; }
    public decimal AAV { get; set; }
}
