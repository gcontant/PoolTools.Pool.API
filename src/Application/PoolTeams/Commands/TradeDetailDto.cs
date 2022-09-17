using Application.PoolTeams.Queries;

namespace Application.PoolTeams.Commands;

public sealed class TradeDetailDto
{
    public int TradePartnerTeamId { get; set; }
    public IList<TradedPlayerDto>? TradedPlayers { get; set; }
    public IList<TradedPlayerDto>? ReceivedPlayers { get; set; }
    public IList<DraftPickDto>? TradedPicks { get; set; }
    public IList<DraftPickDto>? ReceivedPicks { get; set; }
}
