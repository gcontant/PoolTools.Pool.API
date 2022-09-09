namespace PoolTools.Pool.API.Domain.Exceptions;

public class UnsupportedPlayerTradeException : Exception
{
    public UnsupportedPlayerTradeException(Player tradedPlayer, string poolTeamName) : 
        base($"Team {poolTeamName} can't trade player {tradedPlayer.FullName} because he doesn't have him on his roster.")
    {
    }

    public UnsupportedPlayerTradeException(string poolTeamName, string tradePartnerPoolName) :
        base($"Team {poolTeamName} can't trade players to {tradePartnerPoolName} because he doesn't have one or more of the traded players on his roster.")
    {
    }
}
