namespace PoolTools.Pool.API.Domain.Entities;

public class PoolTeam : BaseEntity
{
    private const int maxRound = 5;
    private const int maxPosition = 12;

    public string Name { get; set; }
    //TODO: convert to an entity (name, email, etc.)
    public string Owner { get; set; }

    public IList<DraftPick> DraftPicks { get; set; } = new List<DraftPick>();
    public IList<Player> Roster { get; private set; } = new List<Player>();

    // Empty constructor for EF
    private PoolTeam()
    { }

    public PoolTeam(string name, string owner, IList<DraftPick> draftPicks)
    {
        Name = name;
        Owner = owner;

        DraftPicks = draftPicks;
    }

    public void Trade(PoolTeam tradePartner, IEnumerable<Player> playersTraded, IEnumerable<Player> playerReceived, IEnumerable<DraftPick> draftPicksTraded, IEnumerable<DraftPick> draftPicksReceived)
    {
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

    public void Draft(Player player)
    {
        if (Roster.Contains(player))
        {
            throw new UnsupportedPlayerDraftException(Id, player);
        }

        Roster.Add(player);
    }

    public void Release(Player player)
    {
        if (!Roster.Contains(player) )
        {
            throw new UnsupportedPlayerReleaseException(Id ,player);
        }

        Roster.Remove(player);
    }
}