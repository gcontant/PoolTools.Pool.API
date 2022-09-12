namespace PoolTools.Pool.API.Domain.Entities;

public record TeamId(int Value) : StronglyTypedId<int>(Value);
public class PoolTeam : BaseEntity<TeamId, int>
{
    public string Name { get; set; }
    //TODO: convert to an entity (name, email, etc.)
    public string Owner { get; set; }

    public IList<DraftPick> DraftPicks { get; set; } = new List<DraftPick>();
    public IList<Player> Roster { get; set; } = new List<Player>();

    public PoolTeam(TeamId id,string name, string owner)
    {
        Id = id;
        Name = name;
        Owner = owner;
    }

    //TODO: Move to Application layer??
    public void Trade(PoolTeam tradePartner, IEnumerable<Player> playersTraded, IEnumerable<Player> playerReceived, IEnumerable<DraftPick> draftPicksTraded, IEnumerable<DraftPick> draftPicksReceived)
    {
        //TODO: Introduce TradeValidator
        if (playersTraded.Any(p => !Roster.Contains(p)))
        {
            throw new UnsupportedPlayerTradeException(Name, tradePartner.Name);
        }

        if (playerReceived.Any(p => !Roster.Contains(p)))
        {
            throw new UnsupportedPlayerTradeException( tradePartner.Name, Name);
        }

        if (draftPicksTraded.Any(p => !DraftPicks.Contains(p)))
        {
            throw new UnsupportedDraftPickTradeException(Name, tradePartner.Name);
        }

        if (draftPicksReceived.Any(p => !tradePartner.DraftPicks.Contains(p)))
        {
            throw new UnsupportedDraftPickTradeException(tradePartner.Name, Name);
        }

        foreach (var player in playersTraded)
        {
            Roster.Remove(player);
            tradePartner.Roster.Add(player);
        }

        foreach (var player in playerReceived)
        {
            Roster.Add(player);
            tradePartner.Roster.Remove(player);
        }

        foreach (var draftPick in draftPicksTraded)
        {
            DraftPicks.Remove(draftPick);
            tradePartner.DraftPicks.Add(draftPick);
        }

        foreach (var draftPick in draftPicksReceived)
        {
            DraftPicks.Add(draftPick);
            tradePartner.DraftPicks.Remove(draftPick);
        }
    }

}