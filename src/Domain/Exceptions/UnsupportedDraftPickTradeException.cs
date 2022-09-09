namespace PoolTools.Pool.API.Domain.Exceptions;

public class UnsupportedDraftPickTradeException : Exception
{
    public UnsupportedDraftPickTradeException(string poolTeamName, string tradePartnerPoolName) :
        base($"Team {poolTeamName} can't trade players to {tradePartnerPoolName} because he doesn't have one or more of the traded players on his roster draft picks.")
    {

    }
}
